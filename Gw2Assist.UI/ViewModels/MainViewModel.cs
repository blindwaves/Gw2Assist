using Caliburn.Micro;

namespace Gw2Assist.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;

        public MainViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;
        }

        public void Close()
        {
            this.TryClose();
        }

        public void OpenAbout()
        {
            this.windowManager.ShowWindow(new AboutViewModel());
        }

        public void OpenWvwObjectives()
        {
            this.windowManager.ShowWindow(new Wvw.ObjectivesViewModel(this.eventAggregator));
        }
    }
}
