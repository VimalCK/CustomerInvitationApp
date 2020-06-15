using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common
{
    public sealed class LocalStorageService : IStorageService
    {
        private readonly string location;
        public LocalStorageService(string path)
        {
            location = path;
        }
        public async Task<IEnumerable<T>> GetDataAsync<T>()
        {
            try
            {
                var data = new List<T>();
                if (!File.Exists(location)) return data;
                string line;
                using (var reader = new StreamReader(location))
                {
                    while (true)
                    {
                        line = await reader.ReadLineAsync();
                        if (line == null) break;
                        data.Add(JsonSerializer.Deserialize<T>(line));
                    }
                }

                return data;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Not able to retreive information.");
            }
        }

        public async Task<bool> SaveDataAsync<T>(IEnumerable<T> data)
        {
            try
            {
                if (File.Exists(ConfigurationManager.Settings.OutputFile))
                    File.Delete(ConfigurationManager.Settings.OutputFile);

                using (StreamWriter writer = new StreamWriter(ConfigurationManager.Settings.OutputFile, true))
                {
                    foreach (var item in data)
                    {
                        var jsonContent = JsonSerializer.Serialize(item);
                        await writer.WriteLineAsync(jsonContent);
                    }
                }

                return true;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Not able to save data");
            }
        }
    }
}