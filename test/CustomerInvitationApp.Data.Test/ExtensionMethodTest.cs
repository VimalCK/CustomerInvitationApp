using System;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Test;
using CustomerInvitationApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerInvitationApp.Data.Test
{
    [TestClass]
    public class ExtensionMethodTest
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
        }

        [TestMethod]
        public async Task GetDistanceFromMainOfficeAsync_ShouldNot_throw_exception_for_null_parameters()
        {
            var customer = new Customer { };
            var distance = await customer.GetDistanceFromMainOfficeAsync(null);

            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public async Task GetDistanceFromMainOfficeAsync_Should_return_accurate_distance()
        {
            var customer = new Customer
            {
                Id = 1,
                Name = "Test",
                Latitude = "52.986375",
                Longitude = "-6.043701"
            };

            var distance = await customer.GetDistanceFromMainOfficeAsync(ConfigurationManager.Settings.OfficeLocation);

            Assert.AreEqual(42, distance);
        }
    }
}
