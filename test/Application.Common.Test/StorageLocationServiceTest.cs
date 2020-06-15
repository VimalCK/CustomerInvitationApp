using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Common.Test
{
    [TestClass]
    public class StorageLocationServiceTest
    {

        [TestMethod]
        public void StorageLocationService_Should_return_null_when_empty_path()
        {
            var storageService = StorageLocationService.GetStorageService(null);

            Assert.IsNull(storageService);
        }

        [TestMethod]
        public void StorageLocationService_Should_return_LocalStorageService_for_localpath()
        {
            Helper.CreateDummyCustomers();
            var storageService = StorageLocationService.GetStorageService(ConfigurationManager.Settings.OutputFile);

            Assert.IsInstanceOfType(storageService, typeof(LocalStorageService));
        }

        [TestMethod]
        public void StorageLocationService_Should_return_OnlineStorageService_for_validUri()
        {
            var storageService = StorageLocationService.GetStorageService("https://google.com");

            Assert.IsInstanceOfType(storageService, typeof(OnlineStorageService));
        }
    }
}