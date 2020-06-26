using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Application.ShoppingVan.Queries.CurrentCustomerVan;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ShoppingVanTest.Queries
{

    using static ShoppingVanTesting;

    public class CurrentCustomerVanTest : ShoppingVanTestBase
    {
        [Test]
        public async Task ShouldGetCurrentCustomerVan()
        {
            // Arrange
            var currentCustomerId = await RunAsDefaultUserAsync();

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

            // Assert
            currentCustomerVan.TotalItemsCount.Should().Be(1);
            currentCustomerVan.TotalPrice.Should().Be(addUnitToCommand.SellingPrice + (addUnitToCommand.SellingPrice * 0.14f));
            currentCustomerVan.CustomerId.Should().Be(currentCustomerId);
        }

    }
}
