using System;
using System.Collections.Generic;
using System.Windows;

using Caliburn.Micro;

using Gw2Assist.Core.Scheduler.Jobs;

namespace Gw2Assist.UI
{
    public class Setup : BootstrapperBase
    {
        private readonly SimpleContainer simpleContainer = new SimpleContainer();

        public Setup()
        {
            Initialize();

            this.InitializeComponent();
        }

        protected override void Configure()
        {
            this.simpleContainer.Singleton<IWindowManager, WindowManager>();
            this.simpleContainer.Singleton<IEventAggregator, EventAggregator>();

            this.simpleContainer.PerRequest<ViewModels.AboutViewModel>();
            this.simpleContainer.PerRequest<ViewModels.ShellViewModel>();
            this.simpleContainer.PerRequest<ViewModels.Wvw.ObjectivesViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            this.simpleContainer.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.simpleContainer.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = this.simpleContainer.GetInstance(service, key);
            if (instance != null) return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ViewModels.ShellViewModel>();
        }

        private void InitializeComponent()
        {
            Core.Cache.Repository.Instance.Initialize();

            var eventAggregator = this.simpleContainer.GetInstance<IEventAggregator>();

            // Subscribe and publish any completed jobs and start the scheduler.
            var scheduler = Core.Scheduler.Dispatcher.Instance;
            foreach (var job in scheduler.Jobs)
            {
                job.OnCompleted += () =>
                {
                    // HACK: Will break if the class name changes.
                    switch (job.Name)
                    {
                        case "Gw2CheckAvatarLocation":
                            eventAggregator.PublishOnUIThread((Gw2CheckAvatarLocation)job);
                            break;
                        default:
                            eventAggregator.PublishOnUIThread(job);
                            break;
                    }
                };
            }
            scheduler.Start();
        }
    }
}
