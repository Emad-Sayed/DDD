using System.Threading.Tasks;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.DeleteProduct;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Commands
{
    using static ProductCatalogTesting;

    public class DeleteProductTest : ProductCatalogTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new DeleteProductCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldDeleteProduct()
        {
            // Arrange

            // Create product brand
            var brand = await CreateAsync(new Brand("Test Brand Delete Brand"));

            // Create product category
            var productCategory = await CreateAsync(new ProductCategory("Test ProductCategory Delete Product category"));

            var createProductCommand = new CreateProductCommand
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

            var productToAddId = await SendAsync(createProductCommand);

            var deleteProductCommand = new DeleteProductCommand
            {
                ProductId = productToAddId
            };

            // Act
            await SendAsync(deleteProductCommand);
            var results = FluentActions.Invoking(() => SendAsync(deleteProductCommand));

            // Assert
            results.Should().Throw<ProductNotFoundException>();
        }

    }
}
