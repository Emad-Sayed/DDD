using System;
using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Domain.ProductCatalog.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Queries
{

    using static Testing;

    public class ProductByIdTest : TestBase
    {

        [Test]
        public async Task ShouldGetProductById()
        {
            // Arrange

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

            var productByIdQuery = new ProductByIdQuery
            {
                ProductId = productId
            };

            var product = await SendAsync(productByIdQuery);

            product.Should().NotBeNull();
            product.Id.Should().Be(productId);
        }

        [Test]
        public void ShouldThrowProductNotFoundException()
        {
            // Arrange

            // Product By Id Query
            var productByIdQuery = new ProductByIdQuery()
            {
                // random product Id
                ProductId = Guid.NewGuid().ToString()
            };

            // Act
            var results = FluentActions.Invoking(() => SendAsync(productByIdQuery));

            // Assert
            results.Should().Throw<ProductNotFoundException>();
        }

    }
}
