using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Application.Common.Test
{
    public class Helper
    {
        private static string AppSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        public static string OutputPath => Path.Combine(Directory.GetCurrentDirectory(), "customers.txt");

        public static void InitializeApplicationSettings()
        {
            ClearApplicationSettings();

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartObject();
                    writer.WriteStartObject("Settings");
                    writer.WriteString("Title", "Customer Invitation App");
                    writer.WriteNumber("Distance", 100);
                    writer.WriteString("OutputFile", "output.txt");
                    writer.WriteStartObject("OfficeLocation");
                    writer.WriteNumber("Latitude", 53.339428);
                    writer.WriteNumber("Longitude", -6.257664);
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                string json = Encoding.UTF8.GetString(stream.ToArray());
                File.WriteAllText(AppSettingsPath, json);
            }
        }

        public static void CreateCustomers()
        {
            var customers = new string[] {
                "{\"latitude\":\"52.986375\",\"longitude\":\"-6.043701\",\"user_id\":12,\"name\":\"Christina McArdle\"}",
                "{\"latitude\":\"51.92893\",\"longitude\":\"-10.27699\",\"user_id\":1,\"name\":\"Alice Cahill\"}",
                "{\"latitude\":\"53.2451022\",\"longitude\":\"-6.238335\",\"user_id\":7,\"name\":\"Ian Kehoe\"}",
                "{\"latitude\":\"53.1302756\",\"longitude\":\"-6.2397222\",\"user_id\":5,\"name\":\"Nora Dempsey\"}"
            };

            ClearOutputFiles();

            using (var stream = new StreamWriter(OutputPath, true))
            {
                foreach (var item in customers)
                {
                    stream.WriteLine(item);
                }
            }
        }

        public static void CreateDummyCustomers()
        {
            ClearOutputFiles();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "customers.txt"), "test");
        }

        public static void ClearApplicationSettings()
        {
            if (File.Exists(AppSettingsPath))
                File.Delete(AppSettingsPath);
        }


        public static void ClearOutputFiles()
        {
            if (File.Exists(OutputPath)) File.Delete(OutputPath);
        }
    }
}