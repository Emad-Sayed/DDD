using Application.CustomerManagment.Commands.CreateCustomer;
using Application.OrderManagment.Commands.PlaceOrder;
using Application.OrderManagment.Queries.OrderById;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.ShoppingVan.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.OrderManagment.Commands
{
    using static Testing;

    public class PlaceOrderTest : TestBase
    {
        [Test]
        public async Task ShouldPlaceOrder()
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

            // Act

            // Place Order Command
            var placeOrderCommand = new PlaceOrderCommand();
            var orderId = await SendAsync(placeOrderCommand);

            // Get Order By Id Query
            var orderByIdQuery = new OrderByIdQuery { OrderId = orderId };
            var order = await SendAsync(orderByIdQuery);

            // Assert
            order.Should().NotBeNull();
            order.OrderItems.Count.Should().Be(1);
        }

        [Test]
        public async Task ShouldThrowEmptyShoppingVanException()
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

            // Act

            // Place Order Command
            var placeOrderCommand = new PlaceOrderCommand();

            // Assert

            // Shipp Order Command
            FluentActions.Invoking(() => SendAsync(placeOrderCommand)).Should().Throw<EmptyShoppingVanException>();
        }
    }
}