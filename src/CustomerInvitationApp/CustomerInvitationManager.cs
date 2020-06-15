using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using CustomerInvitationApp.Data;
using CustomerInvitationApp.Model;

namespace CustomerInvitationApp
{
    public sealed class CustomerInvitationManager
    {
        private ICustomerService customerService;
        private IOutputWriter[] outputWriters;

        public CustomerInvitationManager(string customerFileLocation)
        {
            var storageService = StorageLocationService.GetStorageService(customerFileLocation);
            customerService = new CustomerRepository(storageService);
            outputWriters = new IOutputWriter[] {
                new ConsoleWriter(),
                new CustomerOutputFileWriter(customerService)
                };
        }

        public async Task PrepareInvitationListAsync()
        {
            var customers = await customerService.GetCustomersWithinSpecificDistanceAsync(
                                  ConfigurationManager.Settings.OfficeLocation,
                                  ConfigurationManager.Settings.Distance);

            foreach (var writer in outputWriters)
            {
                await writer.CreateOutputAsync(customers.ToArray());
            }
        }
    }
}