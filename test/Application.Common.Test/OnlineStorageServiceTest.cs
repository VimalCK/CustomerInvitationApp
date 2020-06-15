using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerInvitationApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Common.Test
{
    [TestClass]
    public class OnlineStorageServiceTest
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
        public async Task OnlineStorageService_ShouldNot_ThrowException_For_Invalid_Uri_Source()
        {
            var storage = new OnlineStorageService(new Uri("https://www.google.com"));

            await Assert.ThrowsExceptionAsync<Exception>(async () => await storage.GetDataAsync<Customer>());
        }

        [TestMethod]
        public async Task OnlineStorageService_Should_GetData_From_ValidSource()
        {
            var storageService = new OnlineStorageService(new Uri("https://s3.amazonaws.com/intercom-take-home-test/customers.txt"));
            var result = await storageService.GetDataAsync<Customer>();

            Assert.IsTrue(result.Count() > 0);
        }

         [TestMethod]
        public async Task OnlineStorageService_Should_SaveData_WithoutAnyError()
        {
            var data = new string[] { "one", "two" };
            var storage = new OnlineStorageService(new Uri("http://localhost"));
            var result = await storage.SaveDataAsync(data);

            Assert.IsTrue(result);
        }
    }
}