using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
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

    public class AddUnitTest : ProductCatalogTestBase
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
        public async Task ShouldAddUnitToProduct()
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

            var listProductsUnitsQuery = new ListUnitsByProductsIdsQuery
            {
                ProductsIds = new List<string> { productId }
            };

            var currentProductUnits = await SendAsync(listProductsUnitsQuery);

            currentProductUnits.Should().OnlyContain(x =>
                x.Id == unitId &&
                x.ProductId == addUnitToCommand.ProductId &&
                x.SellingPrice.Equals(addUnitToCommand.SellingPrice) &&
                x.ContentCount == addUnitToCommand.ContentCount &&
                x.Price.Equals(addUnitToCommand.Price) &&
                x.Count == addUnitToCommand.Count &&
                x.IsAvailable == addUnitToCommand.IsAvailable &&
                x.Name == addUnitToCommand.Name &&
                x.Weight.Equals(addUnitToCommand.Weight)
                );
        }

        [Test]
        public void ShouldThrowProductNotFoundException()
        {
            // Arrange

            // Add unit to product
            var addUnitToCommand = new AddUnitCommand
            {
                // random product Id
                ProductId =  Guid.NewGuid().ToString(),
                SellingPrice = 92,
                ContentCount = 2,
                Price = 33,
                Count = 6,
                IsAvailable = true,
                Name = "Test Unit",
                Weight = 44
            };

            // Act
            var results = FluentActions.Invoking(() => SendAsync(addUnitToCommand));

            // Assert
            results.Should().Throw<ProductNotFoundException>();
        }

    }
}
