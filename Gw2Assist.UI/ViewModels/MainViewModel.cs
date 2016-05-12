using Caliburn.Micro;

namespace Gw2Assist.UI.ViewModels
{
    public class MainViewModel : Conductor<IScreen>, IConductActiveItem
    {
        private readonly IWindowManager windowManager;

        public MainViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        public void Close()
        {
            this.TryClose();
        }

        public void OpenAbout()
        {
            this.windowManager.ShowWindow(IoC.Get<AboutViewModel>());
        }

        public void OpenWvwObjectives()
        {
            this.windowManager.ShowWindow(IoC.Get<Wvw.ObjectivesViewModel>());
        }
    }
}
