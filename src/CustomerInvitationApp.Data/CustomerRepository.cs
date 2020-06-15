using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Model;
using CustomerInvitationApp.Model;

namespace CustomerInvitationApp.Data
{
    public class CustomerRepository : ICustomerService
    {
        private readonly IStorageService storageService;

        public CustomerRepository(IStorageService storageService)
        {
            if (storageService == null) throw new Exception("No valid Storage found");
            this.storageService = storageService;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await storageService.GetDataAsync<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithinSpecificDistanceAsync(Location location, int distance)
        {
            var customers = await storageService.GetDataAsync<Customer>();
            var customersWithinDistance = new List<Customer>();
            foreach (var customer in customers)
            {
                var d = await customer.GetDistanceFromMainOfficeAsync(location);
                if (d <= distance) customersWithinDistance.Add(customer);
            }

            customersWithinDistance.Sort();
            return customersWithinDistance;
        }

        public async Task<bool> SaveCustomersAsync(IEnumerable<Customer> customers)
        {
            return await storageService.SaveDataAsync(customers);
        }
    }
}
