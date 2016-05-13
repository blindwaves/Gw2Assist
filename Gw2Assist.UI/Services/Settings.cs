using System;

namespace Gw2Assist.UI.Services
{
    public class Settings
    {
        // TODO: Should this be in the service?
        public enum Name
        {
            Gw2ResidentWorld
        }

        public T Get<T>(Name settingName)
        {
            var settingStringName = Enum.GetName(typeof(Name), settingName);
            var setting = Properties.Settings.Default[settingStringName];

            if (setting == null)
            {
                throw new ArgumentException("Setting \"" + settingName + "\" not found.");
            }
            else if (setting.GetType() != typeof(T))
            {
                throw new InvalidCastException("Unable to cast value to " + setting.GetType());
            }

            return (T)setting;
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }

        public void Update<T>(Name settingName, T value)
        {
            var setting = this.Get<T>(settingName); // Check if setting exists.
            var settingStringName = Enum.GetName(typeof(Name), settingName);
            Properties.Settings.Default[settingStringName] = value;
            this.Save();
        }
    }
}
