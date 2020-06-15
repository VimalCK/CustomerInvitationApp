using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common
{
    public sealed class OnlineStorageService : IStorageService
    {
        private readonly Uri uri;
        public OnlineStorageService(Uri uri)
        {
            this.uri = uri;
        }
        public async Task<IEnumerable<T>> GetDataAsync<T>()
        {
            try
            {
                var data = new List<T>();
                using (var client = new HttpClient())
                {
                    var result = await client.GetStringAsync(uri);
                    if (result != null)
                    {
                        var records = result.Split('\n');
                        foreach (var record in records)
                        {
                            data.Add(JsonSerializer.Deserialize<T>(record));
                        }
                    }
                }

                return data;
            }
            catch (System.Exception)
            {
                throw new Exception("Not able to retreive information");
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
                throw new Exception("Not able to save data");
            }
        }
    }
}