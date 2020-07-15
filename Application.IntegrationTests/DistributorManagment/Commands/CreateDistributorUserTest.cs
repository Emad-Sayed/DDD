using Application.DistributorManagment.Commands.CreateDistributor;
using Application.DistributorManagment.Commands.CreateDistributorUser;
using Domain.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.DistributorManagment.Commands
{
    using static Testing;

    public class CreateDistributorUserTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new CreateDistributorUserCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        //[Test]
        //public async Task ShouldCreateDistributorUser()
        //{
        //    // Arrange
        //    // Create Distributor
        //    var createDistributorCommand = new CreateDistributorCommand
        //    {
        //        Name = "Test Distributor",
        //        Area = "Nasr City",
        //        City = "Cairo"
        //    };
        //    var distributorId = await SendAsync(createDistributorCommand);


        //    // Act

        //    // Create Distributor User
        //    var createDistributorUserCommand = new CreateDistributorUserCommand
        //    {
        //        DistributorId = distributorId,
        //        FullName = "Mohamed Test User",
        //        Email = Guid.NewGuid().ToString() + "brimo@mailinator.com"
        //    };

        //    await SendAsync(createDistributorUserCommand);
        //}
    }
}
