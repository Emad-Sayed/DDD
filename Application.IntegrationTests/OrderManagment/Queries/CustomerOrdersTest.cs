using Application.CustomerManagment.Commands.CreateCustomer;
using Application.OrderManagment.Commands.PlaceOrder;
using Application.OrderManagment.Queries.CustomerOrders;
using Application.OrderManagment.Queries.OrderById;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.OrderManagment.Queries
{
    using static Testing;

    public class CustomerOrdersTest : TestBase
    {
        [Test]
        public async Task ShouldGetCustomerOrder()
        {
            // Arrange
            var accountId = await RunAsDefaultUserAsync();

            var createCustomerCommand = new CreateCustomerCommand
            {
                AccountId = accountId,
                City = "Test City",
                Area = "Test Area",
                ShopName = "Test Shop Name",
                ShopAddress = "Test Shop address",
                LocationOnMap = "Test LocationOnMap"
            };

            await SendAsync(createCustomerCommand);

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

            // AddItem To Shopping Van
            var addItemToVanCommand = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = unitId
            };

            await SendAsync(addItemToVanCommand);
            await SendAsync(addItemToVanCommand);

            // Place Order Command
            var placeOrderCommand = new PlaceOrderCommand();
            var orderId = await SendAsync(placeOrderCommand);

            // Act

            // Get Order By Id Query
            var customerOrdersQuery = new CustomerOrdersQuery { CustomerId = accountId, OrderStatuses = new List<OrderStatus> { OrderStatus.Placed } };
            var customerOrders = await SendAsync(customerOrdersQuery);

            // Assert
            customerOrders.Data.Should().NotBeNull();
            customerOrders.Data.Count.Should().Be(1);
        }
    }
}
