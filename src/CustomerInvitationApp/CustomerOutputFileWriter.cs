using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using CustomerInvitationApp.Data;
using CustomerInvitationApp.Model;

namespace CustomerInvitationApp
{
    public class CustomerOutputFileWriter : IOutputWriter
    {
        private ICustomerService customerService;
        public CustomerOutputFileWriter(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        public async Task<bool> CreateOutputAsync<T>(T[] data)
        {
            if (await customerService.SaveCustomersAsync(data.Cast<Customer>()))
            {
                Console.WriteLine($"Saved to {ConfigurationManager.Settings.OutputFile}");
                return true;
            }
            else
            {
                throw new Exception("Failed to prepare customer invitation list;");
            }
        }
    }
}