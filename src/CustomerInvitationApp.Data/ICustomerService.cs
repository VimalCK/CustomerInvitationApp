using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Model;
using CustomerInvitationApp.Model;

namespace CustomerInvitationApp.Data
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersWithinSpecificDistanceAsync(Location location, int distance);
        Task<bool> SaveCustomersAsync(IEnumerable<Customer> customers);
    }
}