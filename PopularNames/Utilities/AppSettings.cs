using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PopularNames.Utilities
{
    public static class AppSettings
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                return appSettings[key] ?? string.Empty;
            }
            catch (ConfigurationErrorsException)
            {
                // logger.Error("Error reading app settings");
            }

            return string.Empty;
        }

    }
}