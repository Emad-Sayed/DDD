using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Application.ShoppingVan.Commands.RemoveItemFromVan;
using Domain.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ShoppingVanTest.Commands
{

    using static Testing;

    public class RemoveItemFromVanTest : TestBase
    {

        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new RemoveItemFromVanCommand();

            FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldRemoveItemFromVan()
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
            var addItemToVanCommand = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = unitId
            };
            await SendAsync(addItemToVanCommand);
            await SendAsync(addItemToVanCommand);

            // remove item from Shopping Van
            var command = new RemoveItemFromVanCommand
            {
                ProductId = productId,
                UnitId = unitId
            };

            var productCount = await SendAsync(command);

            // Act

            // Assert
            productCount.Should().Be(1);
        }

    }
}
