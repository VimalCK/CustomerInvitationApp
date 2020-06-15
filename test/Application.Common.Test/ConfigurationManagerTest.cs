using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Common.Test
{
    [TestClass]
    public class ConfigurationManagerTest
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
        public void ConfigurationManager_Should_InitializeApplicationSettings_WithoutAnyError()
        {
            Assert.AreEqual("Customer Invitation App", ConfigurationManager.Settings.Title);
            Assert.AreEqual(100, ConfigurationManager.Settings.Distance);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ConfigurationManager_Should_ThrowException_When_SettingsFileNotFound()
        {
            Helper.ClearApplicationSettings();

            ConfigurationManager.ReloadConfigurationManager();
        }
    }
}
