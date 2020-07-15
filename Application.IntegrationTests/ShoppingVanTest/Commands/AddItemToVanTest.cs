using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ShoppingVanTest.Commands
{


    using static Testing;

    public class AddItemToVanTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new AddItemToVanCommand();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldAddItemToVan()
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

            // Act
            await SendAsync(command);
            var shoppingVanItemCount = await SendAsync(command);

            // Assert
            shoppingVanItemCount.Should().Be(2);
        }

        [Test]
        public void ShouldThrowProductNotFoundException()
        {
            // AddItem To Shopping Van
            var command = new AddItemToVanCommand
            {
                ProductId = Guid.NewGuid().ToString(),
                UnitId = Guid.NewGuid().ToString()
            };

            // Act

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ProductNotFoundException>();
        }


        [Test]
        public async Task ShouldThrowUnitNotFoundExceptionAsync()
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


            // AddItem To Shopping Van
            var command = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = Guid.NewGuid().ToString()
            };
            // Act
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<UnitNotFoundException>();
        }

    }
}
