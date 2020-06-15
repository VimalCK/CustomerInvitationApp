using Application.Common.Model;
using System;
using System.IO;
using System.Text.Json;

namespace Application.Common
{
    public sealed class ConfigurationManager
    {
        private static Settings settings;
        public static Settings Settings
        {
            get
            {
                if (settings == null)
                    InitializeApplicationSettings();

                return settings;
            }
        }

        public static void ReloadConfigurationManager()
        {
            InitializeApplicationSettings();
        }

        private static void InitializeApplicationSettings()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                var jsonDocument = JsonDocument.Parse(File.ReadAllText(path));
                settings = JsonSerializer.Deserialize<Settings>(jsonDocument.RootElement.GetProperty("Settings").ToString());
                settings.OutputFile = Path.Combine(Directory.GetCurrentDirectory(), settings.OutputFile);
            }
            catch (Exception)
            {
                settings = null;
                throw new Exception("Failed to load settings");
            }
        }
    }
}