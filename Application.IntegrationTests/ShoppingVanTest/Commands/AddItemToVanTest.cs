using Application.Common.Interfaces;
using Application.ProductCatalog.BrandAggregate.Commands.CreateBrand;
using Application.ProductCatalog.ProductAggregate.Commands.AddUnit;
using Application.ProductCatalog.ProductAggregate.Commands.CreateProduct;
using Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory;
using Application.ShoppingVan.Commands.AddItemToVan;
using Domain.Common.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ShoppingVanTest.Commands
{


    using static ShoppingVanTesting;

    public class AddItemToVanTest : ShoppingVanTestBase
    {
        private readonly Mock<ICurrentUserService> _currentUserService;
        public AddItemToVanTest()
        {
            _currentUserService = new Mock<ICurrentUserService>();
        }

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
        public async Task ShouldAddItemToVan()
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
                Weight   = 44
            };

            var unitId = await SendAsync(addUnitToCommand);

            // AddItem To Shopping Van
            var command = new AddItemToVanCommand
            {
                ProductId = productId,
                UnitId = unitId
            };

            // Act
            var shoppingVanItemCound = await SendAsync(command);

            // Assert
            shoppingVanItemCound.Should().BeGreaterThan(0);
        }

    }
}
