using System.Diagnostics;
using System.Reflection;

namespace Gw2Assist.UI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string version;
        public string Version
        {
            get { return this.version; }
            set
            {
                this.version = value;
                NotifyOfPropertyChange(() => this.Version);
            }
        }

        public AboutViewModel()
        {
            this.InitializeData();
        }

        private void InitializeData()
        {
            this.Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }
    }
}
