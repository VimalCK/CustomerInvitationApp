using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Common
{
    public sealed class ConsoleWriter : IOutputWriter
    {
        private static int pageWidth;
        private static int intend = 20;

        public static void PrintHeading(string content)
        {
            if (string.IsNullOrEmpty(content)) throw new Exception("No valid content to print");
            pageWidth = content.Length;
            Console.WriteLine($"{new String('=', pageWidth + intend)}\n");
            if (!(string.IsNullOrEmpty(content))) Console.WriteLine($"{new String(' ', 10)}{content.ToUpper()}\n");
            Console.WriteLine(new String('=', pageWidth + intend));
            PrintSettings();
        }

        private static void PrintSettings()
        {
            Console.WriteLine($"\n SETTINGS : Latitude  {ConfigurationManager.Settings.OfficeLocation.Latitude}");
            Console.WriteLine($"{new String(' ', 11)} Longitude {ConfigurationManager.Settings.OfficeLocation.Longitude}");
            Console.WriteLine($"{new String(' ', 11)} Distance  {ConfigurationManager.Settings.Distance}km");
            Console.WriteLine($"\n{new String('-', pageWidth + intend)}");
        }

        public static void PrintError(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        public async Task<bool> CreateOutputAsync<T>(T[] data)
        {
            try
            {
                var columns = typeof(T).GetProperties().
                Where(p => p.IsDefined(typeof(Header))).
                Select(pageWidth => pageWidth.Name);

                if (columns.Count().Equals(0)) return false;
                Console.WriteLine(new string('-', pageWidth + intend));
                for (int i = -1; i < data.Length; i++)
                {
                    foreach (var item in columns)
                    {
                        if (typeof(T).GetProperty(item).PropertyType == typeof(string))
                        {
                            Console.Write($"{(i < 0 ? item : data[i].GetType().GetProperty(item).GetValue(data[i]))}{new String(' ', 50)}");
                        }
                        else
                        {
                            Console.Write($"{(i < 0 ? item : data[i].GetType().GetProperty(item).GetValue(data[i]))}{new String(' ', 10)}");
                        }
                    }

                    Console.WriteLine($"{(i < 0 ? "\n" + new string('-', pageWidth + intend) : "")}");
                }

                Console.WriteLine(new string('-', pageWidth + intend));
                Console.WriteLine($"({data.Length}) records found.");
                Console.WriteLine(new string('=', pageWidth + intend));
            }
            catch (System.Exception)
            {
                return false;
            }

            return await Task.FromResult<bool>(true);
        }
    }
}