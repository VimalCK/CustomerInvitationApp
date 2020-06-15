using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerInvitationApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Common.Test
{
    [TestClass]
    public class LocalStorageServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Helper.InitializeApplicationSettings();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Helper.ClearApplicationSettings();
            Helper.ClearOutputFiles();
        }

        [TestMethod]
        public async Task LocalStorageService_ShouldNot_ThrowException_For_InvalidSource()
        {
            var storage = new LocalStorageService("test");
            var result = await storage.GetDataAsync<Customer>();

            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public async Task LocalStorageService_Should_GetData_From_ValidSource()
        {
            Helper.CreateCustomers();

            var storageService = new LocalStorageService(Helper.OutputPath);
            var result = await storageService.GetDataAsync<Customer>();

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task LocalStorageService_Should_ThrowException_For_InvalidData()
        {
            Helper.CreateDummyCustomers();
            var storageService = new LocalStorageService(ConfigurationManager.Settings.OutputFile);

            var exception = await Assert.ThrowsExceptionAsync<Exception>(async () => await storageService.GetDataAsync<Customer>());
            Assert.AreEqual("Not able to retreive information.", exception.Message);
        }

        [TestMethod]
        public async Task LocalStorageService_Should_SaveData_WithoutAnyError()
        {
            var data = new string[] { "one", "two" };
            var storage = new LocalStorageService(ConfigurationManager.Settings.OutputFile);
            var result = await storage.SaveDataAsync(data);

            Assert.IsTrue(result);
        }
    }
}