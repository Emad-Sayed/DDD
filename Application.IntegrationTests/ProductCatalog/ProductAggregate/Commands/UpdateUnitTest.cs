using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit;
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

    public class UpdateUnitTest : ProductCatalogTestBase
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
        public async Task ShouldUpdateProductUnit()
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

            // update product unit
            var updateProductUnitCommand = new UpdateUnitCommand
            {
                ProductId = productId,
                SellingPrice = 992,
                ContentCount = 25,
                Price = 333,
                Count = 65,
                IsAvailable = false,
                Name = "Test Unit Update",
                Weight = 4,
                Id = unitId
            };

            await SendAsync(updateProductUnitCommand);

            var listProductsUnitsQuery = new ListUnitsByProductsIdsQuery
            {
                ProductsIds = new List<string> { productId }
            };


            var currentProductUnits = await SendAsync(listProductsUnitsQuery);

            currentProductUnits.Should().OnlyContain(x =>
                x.Id == unitId &&
                x.ProductId == updateProductUnitCommand.ProductId &&
                x.SellingPrice.Equals(updateProductUnitCommand.SellingPrice) &&
                x.ContentCount == updateProductUnitCommand.ContentCount &&
                x.Price.Equals(updateProductUnitCommand.Price) &&
                x.Count == updateProductUnitCommand.Count &&
                x.IsAvailable == updateProductUnitCommand.IsAvailable &&
                x.Name == updateProductUnitCommand.Name &&
                x.Weight.Equals(updateProductUnitCommand.Weight)
                );
        }


        [Test]
        public void ShouldThrowProductNotFoundException()
        {
            // Arrange

            // update product unit
            var updateProductUnitCommand = new UpdateUnitCommand
            {
                ProductId = Guid.NewGuid().ToString(),
                SellingPrice = 992,
                ContentCount = 25,
                Price = 333,
                Count = 65,
                IsAvailable = false,
                Name = "Test Unit Update",
                Weight = 4,
                Id = Guid.NewGuid().ToString()
            };

            // Act
            var results = FluentActions.Invoking(() => SendAsync(updateProductUnitCommand));

            // Assert
            results.Should().Throw<ProductNotFoundException>();
        }

    }
}
