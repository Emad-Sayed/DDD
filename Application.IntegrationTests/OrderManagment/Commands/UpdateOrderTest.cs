using Application.CustomerManagment.Commands.CreateCustomer;
using Application.OrderManagment.Commands.PlaceOrder;
using Application.OrderManagment.Commands.UpdateOrder;
using Application.OrderManagment.Queries.OrderById;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.OrderManagment.Commands
{
    using static Testing;

    public class UpdateOrderTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateOrderCommand();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldUpdateOrder()
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

            // Add first unit to product
            var addFirstUnitCommand = new AddUnitCommand
            {
                ProductId = productId,
                SellingPrice = 92,
                ContentCount = 2,
                Price = 33,
                Count = 6,
                IsAvailable = true,
                Name = "Test First Unit",
                Weight = 44
            };

            var firstUnitId = await SendAsync(addFirstUnitCommand);

            // Add first unit to product
            var addSecondUnitCommand = new AddUnitCommand
            {
                ProductId = productId,
                SellingPrice = 342,
                ContentCount = 24,
                Price = 323,
                Count = 64,
                IsAvailable = true,
                Name = "Test Second Unit",
                Weight = 94
            };

            var secondUnitId = await SendAsync(addSecondUnitCommand);

            // AddItem To Shopping Van
            var addItemToVanCommand = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = firstUnitId
            };

            await SendAsync(addItemToVanCommand);
            await SendAsync(addItemToVanCommand);

            // Place Order Command
            var placeOrderCommand = new PlaceOrderCommand();
            var orderId = await SendAsync(placeOrderCommand);

            // Get Order By Id Query
            var orderByIdQuery = new OrderByIdQuery { OrderId = orderId };
            var order = await SendAsync(orderByIdQuery);
            // Act

            var updateOrderCommand = new UpdateOrderCommand 
            { 
                OrderId = orderId,
                OrderItemId = order.OrderItems.FirstOrDefault(x => x.UnitId == firstUnitId).Id,
                UnitId = secondUnitId,
                UnitCount = 10,
                UnitName = addSecondUnitCommand.Name
            };

            await SendAsync(updateOrderCommand);

            // Get Order By Id Query
            var orderAfterUpdate = await SendAsync(orderByIdQuery);


            // Assert
            order.Should().NotBeNull();
            order.OrderItems.Count.Should().Be(1);
            order.OrderItems.FirstOrDefault(x => x.UnitId == firstUnitId).UnitName.Should().Be(addFirstUnitCommand.Name);
            orderAfterUpdate.OrderItems.FirstOrDefault(x => x.UnitId == secondUnitId).UnitName.Should().Be(addSecondUnitCommand.Name);
        }
    }
}
