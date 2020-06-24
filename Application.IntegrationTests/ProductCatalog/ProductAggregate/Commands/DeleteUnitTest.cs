using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit;
using Application.ProductCatalog.ProductAggregate.Queries.ListUnitsByProductsIds;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Commands
{

    using static ProductCatalogTesting;

    public class DeleteUnitTest : ProductCatalogTestBase
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
        public async Task ShouldDeleteUnitFromProduct()
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

            var deleteProductUnitCommand = new DeleteUnitCommand
            {
                ProductId = productId,
                UnitId = unitId
            };

            await SendAsync(deleteProductUnitCommand);

            var listProductsUnitsQuery = new ListUnitsByProductsIdsQuery
            {
                ProductsIds = new List<string> { productId }
            };

            var currentProductUnits = await SendAsync(listProductsUnitsQuery);

            currentProductUnits.Should().NotContain(x =>
                x.Id == unitId &&
                x.ProductId == addUnitToCommand.ProductId);
        }

        [Test]
        public void ShouldThrowProductNotFoundException()
        {
            // Arrange

            // Add unit to product
            var deleteUnitToCommand = new DeleteUnitCommand
            {
                // random product Id
                ProductId = Guid.NewGuid().ToString(),
                UnitId = Guid.NewGuid().ToString()
            };

            // Act
            var results = FluentActions.Invoking(() => SendAsync(deleteUnitToCommand));

            // Assert
            results.Should().Throw<ProductNotFoundException>();
        }

    }
}
