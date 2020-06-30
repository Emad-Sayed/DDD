using Application.DistributorManagment.Commands.CreateDistributor;
using Application.DistributorManagment.Commands.UpdateDistributor;
using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using FluentAssertions;
using NUnit.Framework;
using Persistence.DistributorManagment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.DistributorManagment.Commands
{
    using static Testing;

    public class UpdateDistributor : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new UpdateDistributorCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldUpdateDistributor()
        {
            // Arrange

            var createDistributorCommand = new CreateDistributorCommand
            {
                Name = "Test Distributor",
                Area = "Nasr City",
                City = "Cairo"
            };

            var distributorId = await SendAsync(createDistributorCommand);


            // Act
            var updateDistributorCommand = new UpdateDistributorCommand
            {
                Id = distributorId,
                Name = "Test Update Distributor",
                Area = "Update Distributor Area",
                City = "Update Distributor City"
            };

            await SendAsync(updateDistributorCommand);
            var distributor = await FindAsync<Distributor, DistributorManagmentContext>(distributorId);

            // Assert
            distributor.Should().NotBeNull();
            distributor.Name.Should().Be(updateDistributorCommand.Name);
            distributor.Address.Area.Should().Be(updateDistributorCommand.Area);
            distributor.Address.City.Should().Be(updateDistributorCommand.City);
        }
    }
}
