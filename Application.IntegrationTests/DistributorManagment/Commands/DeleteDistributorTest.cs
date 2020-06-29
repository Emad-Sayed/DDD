using Application.DistributorManagment.Commands.CreateDistributor;
using Application.DistributorManagment.Commands.DeleteDistributor;
using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using FluentAssertions;
using NUnit.Framework;
using Persistence.DistributorManagment;
using System.Threading.Tasks;

namespace Application.IntegrationTests.DistributorManagment.Commands
{
        using static Testing;

    public class DeleteDistributorTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new DeleteDistributorCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldDeleteDistributor()
        {
            // Arrange

            // Create Distributor
            var createDistributorCommand = new CreateDistributorCommand
            {
                Name = "Test Distributor",
                Area = "Nasr City",
                City = "Cairo"
            };
            var distributorId = await SendAsync(createDistributorCommand);

            // Act

            // Delete Distributor
            var deleteDistributorCommand = new DeleteDistributorCommand
            {
               DistributorId = distributorId
            };
            await SendAsync(deleteDistributorCommand);

            var distributor = await FindAsync<Distributor, DistributorManagmentContext>(distributorId);

            // Assert
            distributor.Should().BeNull();
        }
    }
}
