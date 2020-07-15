using Application.CustomerManagment.Commands.CreateCustomer;
using Domain.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CustomerManagment.Commands
{
     using static Testing;

    public class CreateCustomerTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCustomerCommand();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldCreateCustomer()
        {
            // Arrange
            var accountId = await RunAsDefaultUserAsync();

            var createCustomerCommand = new CreateCustomerCommand
            {
                AccountId = accountId,
                City = "Test City",
                Area = "Test Area",
                ShopName = "Test Shop Name",
                ShopAddress = "Test Shop address",
                LocationOnMap = "Test LocationOnMap"
            };

            var customerId = await SendAsync(createCustomerCommand);

          
            // Act

        

            // Assert
        }
    }
}
