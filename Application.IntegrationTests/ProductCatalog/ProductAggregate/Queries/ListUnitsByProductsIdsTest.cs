using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductAggregate.Queries.ListProducts;
using Application.ProductCatalog.ProductAggregate.Queries.ListUnitsByProductsIds;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.ProductCatalog.ProductAggregate.Queries
{
    using static ProductCatalogTesting;

    public class ListUnitsByProductsIdsTest : ProductCatalogTestBase
    {

        [Test]
        public async Task ShouldListUnitsByProductsIds()
        {
            // Arrange

            // Create product brand
            var brandCommand = new CreateBrandCommand { Name = "Test Brand" };
            var brandId = await SendAsync(brandCommand);

            // Create product category
            var productCategoryCommand = new CreateProductCategoryCommand { Name = "Test Product Category" };
            var productCategoryId = await SendAsync(productCategoryCommand);

            // Create First product 
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

            // Create Second product 
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


            var firstProductId = await SendAsync(createFirstProductCommand);
            var secondProductId = await SendAsync(createSecondProductCommand);

            // Add unit to first product
            var addFirstUnitCommand = new AddUnitCommand
            {
                ProductId = firstProductId,
                SellingPrice = 92,
                ContentCount = 2,
                Price = 33,
                Count = 6,
                IsAvailable = true,
                Name = "Test Unit",
                Weight = 44
            };

            // Add unit to second product
            var addSecondUnitCommand = new AddUnitCommand
            {
                ProductId = secondProductId,
                SellingPrice = 92,
                ContentCount = 2,
                Price = 33,
                Count = 6,
                IsAvailable = true,
                Name = "Test Unit",
                Weight = 44
            };

            var firstUnitId = await SendAsync(addFirstUnitCommand);
            var secondUnitId = await SendAsync(addSecondUnitCommand);




            var listUnitsByProductsIdsQuery = new ListUnitsByProductsIdsQuery { ProductsIds = new List<string> { firstProductId, secondProductId } };
            var productsUnits = await SendAsync(listUnitsByProductsIdsQuery);

            productsUnits.Should().NotBeNull();
            productsUnits.Should().HaveCount(2);
            productsUnits.Should().Contain(x => x.Id == firstUnitId);
            productsUnits.Should().Contain(x => x.Id == secondUnitId);
        }

    }
}
