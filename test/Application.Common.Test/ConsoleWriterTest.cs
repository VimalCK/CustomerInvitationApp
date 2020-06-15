using System;
using System.Threading.Tasks;
using CustomerInvitationApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Common.Test
{

    [TestClass]
    public class ConsoleWriterTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ConsoleWriter_PrintHeading_Should_throw_exception_for_empty_content()
        {
            ConsoleWriter.PrintHeading(null);
        }

        [TestMethod]
        public void ConsoleWriter_PrintError_ShouldNot_throw_exception_for_empty_content()
        {
            ConsoleWriter.PrintError(null);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task ConsoleWriter_PrintList_ShouldNot_throw_exception_for_empty_content()
        {
            var customers = new Customer[3];
            for (int i = 1; i <= 3; i++)
            {
                customers[i - 1] = new Customer { Id = i, Name = $"Customer {i}" };
            }

            var writer = new ConsoleWriter();
            await writer.CreateOutputAsync<Customer>(null);
            Assert.IsTrue(true);

            await writer.CreateOutputAsync<Customer>(customers);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task ConsoleWriter_PrintList_ShouldNot_throw_exception_for_valid_parameters()
        {
            var customers = new Customer[3];
            for (int i = 1; i <= 3; i++)
            {
                customers[i - 1] = new Customer { Id = i, Name = $"Customer {i}" };
            }

           var writer = new ConsoleWriter();
      
            await writer.CreateOutputAsync<Customer>(customers);
            Assert.IsTrue(true);
        }
    }
}