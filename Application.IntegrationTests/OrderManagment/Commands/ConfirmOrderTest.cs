﻿using System;
using System.Threading.Tasks;
using Application.CustomerManagment.Commands.CreateCustomer;
using Application.OrderManagment.Commands.ConfirmOrder;
using Application.OrderManagment.Commands.PlaceOrder;
using Application.OrderManagment.Queries.OrderById;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.Common.Exceptions;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.OrderManagment.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.OrderManagment.Commands
{
    using static Testing;

    public class ConfirmOrderTest : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new ConfirmOrderCommand();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should()
                .Throw<BaseValidationException>();
        }

        [Test]
        public async Task ShouldConfirmOrder()
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

            // Confirm Order Command
            var confirmOrderCommand = new ConfirmOrderCommand { OrderId = orderId };
            await SendAsync(confirmOrderCommand);

            // Get Order By Id Query
            var orderByIdQuery = new OrderByIdQuery { OrderId = orderId };
            var order = await SendAsync(orderByIdQuery);

            // Assert
            order.OrderStatus.Should().Be(OrderStatus.Confirmed);
        }

    }
}
