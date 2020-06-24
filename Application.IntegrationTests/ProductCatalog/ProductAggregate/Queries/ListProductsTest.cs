using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Queries.ListProducts;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Queries
{
    using static ProductCatalogTesting;

    public class ListProductsTest : ProductCatalogTestBase
    {

        [Test]
        public async Task ShouldListProducts()
        {
            // Arrange

            // Create product brand
            var brandCommand = new CreateBrandCommand { Name = "Test Brand" };
            var brandId = await SendAsync(brandCommand);

            // Create product category
            var productCategoryCommand = new CreateProductCategoryCommand { Name = "Test Product Category" };
            var productCategoryId = await SendAsync(productCategoryCommand);

            // Create product 
            var createFirstProductCommand = new CreateProductCommand
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

            // Create product 
            var createSecondProductCommand = new CreateProductCommand
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

            await SendAsync(createFirstProductCommand);
            await SendAsync(createSecondProductCommand);

            var productsQuery = new ListProductsQuery();
            var products = await SendAsync(productsQuery);

            products.Data.Should().NotBeNull();
            products.Data.Should().HaveCount(2);
        }

    }
}
