using Caliburn.Micro;

namespace Gw2Assist.UI.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private readonly IWindowManager windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            this.DisplayName = "GW2 Assist";
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
