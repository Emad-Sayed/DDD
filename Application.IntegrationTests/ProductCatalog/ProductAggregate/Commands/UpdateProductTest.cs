using System.Threading.Tasks;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.UpdateProduct;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using FluentAssertions;
using NUnit.Framework;
using Persistence.ProductCatalog;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Commands
{
    using static Testing;

    public class UpdateProductTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new UpdateProductCommand();

            // Act
            var results = FluentActions.Invoking(() => SendAsync(command));

            // Assert
            results.Should().Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldUpdateProduct()
        {
            // Arrange

            // Create product brand
            var brand = await CreateAsync<Brand, ProductCatalogContext>(new Brand("Test Brand Update Brand"));

            // Create product category
            var productCategory = await CreateAsync<ProductCategory, ProductCatalogContext>(new ProductCategory("Test ProductCategory Update Product category"));

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

            var updateProductCommand = new UpdateProductCommand
            {
                AvailableToSell = false,
                // created brand id
                BrandId = brand.Id.ToString(),
                // created product category id
                ProductCategoryId = productCategory.Id.ToString(),
                Name = "Test Product Name Update",
                PhotoUrl = "Test Product PhotoUrl Update",
                Barcode = "Test Product Barcode Update",
                Id = productToAddId
            };

            var productToUpdateId = await SendAsync(updateProductCommand);
            var product = await FindAsync<Product, ProductCatalogContext>(productToUpdateId);

            // Act


            // Assert
            productToAddId.Should().Be(productToUpdateId);
            product.Should().NotBeNull();
            product.Name.Should().Be("Test Product Name Update");
            product.PhotoUrl.Should().Be("Test Product PhotoUrl Update");
            product.Barcode.Should().Be("Test Product Barcode Update");
            product.AvailableToSell.Should().Be(false);
        }

    }
}
