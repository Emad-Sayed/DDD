using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Application.ShoppingVan.Commands.DeleteCurrentCustomerVan;
using Application.ShoppingVan.Queries.CurrentCustomerVan;
using Domain.ShoppingVan.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ShoppingVanTest.Commands
{
    using static Testing;

    public class DeleteCurrentCustomerVanTest : TestBase
    {
        [Test]
        public async Task ShouldDeleteCurrentCustomerVan()
        {
            // Arrange
            await RunAsDefaultUserAsync();

            // Create product brand
            var brandCommand = new CreateBrandCommand { Name = "Test Brand" };
            var brandId = await SendAsync(brandCommand);

            // Create product category
            var productCategoryCommand = new CreateProductCategoryCommand { Name = "Test Product Category" };
            var productCategoryId = await SendAsync(productCategoryCommand);

            // Create product
            var createProductCommand = new CreateProductCommand
            {
                AvailableToSell = true,
                // created brand id
                BrandId = brandId,
                // created product category id
                ProductCategoryId = productCategoryId,
                Name = "Test Product",
                PhotoUrl = "Test Product",
                Barcode = "Test Product"
            };

            var productId = await SendAsync(createProductCommand);

            // Add unit to product
            var addUnitToCommand = new AddUnitCommand
            {
                ProductId = productId,
                SellingPrice = 92,
                ContentCount = 2,
                Price = 33,
                Count = 6,
                IsAvailable = true,
                Name = "Test Unit",
                Weight = 44
            };

            var unitId = await SendAsync(addUnitToCommand);

            // AddItem To Shopping Van
            var command = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = unitId
            };

            await SendAsync(command);
            // Act

            var getCurrentCustomerVanQuery = new CurrentCustomerVanQuery();
            var currentCustomerVan = await SendAsync(getCurrentCustomerVanQuery);

            var deleteCurrentCustomerVanCommand = new DeleteCurrentCustomerVanCommand();

            await SendAsync(deleteCurrentCustomerVanCommand);

            var currentCustomerVanAfterDeleting = await SendAsync(getCurrentCustomerVanQuery);
            // Assert

            currentCustomerVan.Should().NotBeNull();
            currentCustomerVanAfterDeleting.Should().BeNull();
        }

        [Test]
        public async Task ShouldThrowEmptyShoppingVanException()
        {
            // Arrange
            await RunAsDefaultUserAsync();

            // Act

            var deleteCurrentCustomerVanCommand = new DeleteCurrentCustomerVanCommand();

            FluentActions.Invoking(() => SendAsync(deleteCurrentCustomerVanCommand)).Should().Throw<EmptyShoppingVanException>();
        }
    }
}