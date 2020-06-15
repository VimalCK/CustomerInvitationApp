using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerInvitationApp.Data.Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Helper.InitializeApplicationSettings();
            Helper.CreateCustomers();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Helper.ClearApplicationSettings();
            Helper.ClearOutputFiles();
        }

        [TestMethod]
        public void CustomerRepositery_should_throw_exception_for_invalid_location()
        {
            var path = @"C:\customers.txt";
            var storageService = StorageLocationService.GetStorageService(path);
            Assert.ThrowsException<Exception>(() => new CustomerRepository(storageService));
        }

        [TestMethod]
        public async Task CustomerRepository_should_return_data_for_valid_source()
        {
            var storageService = StorageLocationService.GetStorageService(Helper.OutputPath);
            var repository = new CustomerRepository(storageService);
            var result = await repository.GetCustomersAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public async Task CustomerRepository_should_return_data_with_specific_distance()
        {
            var storageService = StorageLocationService.GetStorageService(Helper.OutputPath);
            var repository = new CustomerRepository(storageService);
            var result = await repository.GetCustomersWithinSpecificDistanceAsync(ConfigurationManager.Settings.OfficeLocation, 100);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task CustomerRepository_should_return_SortedData_with_specific_distance()
        {
            var storageService = StorageLocationService.GetStorageService(Helper.OutputPath);
            var repository = new CustomerRepository(storageService);
            var result = await repository.GetCustomersWithinSpecificDistanceAsync(ConfigurationManager.Settings.OfficeLocation, 100);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(result.ElementAt(0).Id, 5);
            Assert.AreEqual(result.ElementAt(2).Id, 12);
        }
    }
}