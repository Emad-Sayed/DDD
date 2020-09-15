using Application.DistributorManagment.Commands.CreateDistributor;
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

    public class CreateDistributorTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new CreateDistributorCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldCreateDistributor()
        {
            // Arrange

            var createDistributorCommand = new CreateDistributorCommand
            {
                Name = "Test Distributor"
            };

            // Act
            var distributorId = await SendAsync(createDistributorCommand);
            var distributor = await FindAsync<Distributor, DistributorManagmentContext>(distributorId);

            // Assert
            distributor.Should().NotBeNull();
            distributor.Created.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }
    }
}
