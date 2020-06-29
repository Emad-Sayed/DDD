using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using FluentAssertions;
using NUnit.Framework;
using Persistence.ProductCatalog;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Commands
{

    using static Testing;

    public class CreateProductTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new CreateProductCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldCreateProduct()
        {
            // Arrange

            // Create product brand
            var brand = await CreateAsync<Brand, ProductCatalogContext>(new Brand("Test Brand"));

            // Create product category
            var productCategory = await CreateAsync<ProductCategory, ProductCatalogContext>(new ProductCategory("Test ProductCategory"));

            var command = new CreateProductCommand
            {
                AvailableToSell = true,
                // created brand id
                BrandId = brand.Id.ToString(),
                // created product category id
                ProductCategoryId = productCategory.Id.ToString(),
                Name = "Test Product",
                PhotoUrl = "Test Product",
                Barcode = "Test Product"
            };

            // Act
            var productId = await SendAsync(command);
            var product = await FindAsync<Product, ProductCatalogContext>(productId);

            // Assert
            product.Should().NotBeNull();
            product.CreatedDateUtc.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }

    }
}
