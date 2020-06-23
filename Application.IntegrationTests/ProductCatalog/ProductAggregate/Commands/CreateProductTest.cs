using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Commands
{

    using static ProductCatalogTesting;

    public class CreateProductTest : ProductCatalogTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateProductCommand();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldCreateProduct()
        {
            // Arrange

            // Create product brand
            var brand = await CreateAsync(new Brand("Test Brand"));

            // Create product category
            var productCategory = await CreateAsync(new ProductCategory("Test ProductCategory"));

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
            var product = await FindAsync<Product>(productId);

            // Assert
            product.Should().NotBeNull();
            product.CreatedDateUtc.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }

    }
}
