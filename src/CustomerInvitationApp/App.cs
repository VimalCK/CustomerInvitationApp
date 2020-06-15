using Application.Common;
using System;
using System.Threading.Tasks;

namespace CustomerInvitationApp
{
    public sealed class App
    {
        async static Task Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var app = new App();
                    app.Initialize();

                    Console.Write("Enter local/remote file :");
                    var fileName = Console.ReadLine();

                    var invitationManager = new CustomerInvitationManager(fileName);
                    await invitationManager.PrepareInvitationListAsync();
                }
                catch (System.Exception ex)
                {
                    ConsoleWriter.PrintError(ex.Message);
                }

                Console.WriteLine("Press Y key to continue...");
                if (!Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
                    break;

            }
        }

        private void Initialize()
        {
            try
            {
                ConsoleWriter.PrintHeading(ConfigurationManager.Settings.Title);
            }
            catch (System.Exception)
            {
                throw new Exception("Failed to load settings");
            }
        }
    }
}
