using Bogus;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.CustomerManagment;
using Persistence.DistributorManagment;
using Persistence.OfferManagment;
using Persistence.OrderManagment;
using Persistence.ProductCatalog;
using Persistence.ShoppingVan;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Helpers
{
    public static class DataSeeder
    {
        public static IHost SeedProductCatalogData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var productCatalogContext = scope.ServiceProvider.GetService<ProductCatalogContext>();
                var orderContext = scope.ServiceProvider.GetService<OrderContext>();
                var customerManagmentContext = scope.ServiceProvider.GetService<CustomerManagmentContext>();
                var shoppingVanContext = scope.ServiceProvider.GetService<ShoppingVanContext>();
                var distributorManagmentContext = scope.ServiceProvider.GetService<DistributorManagmentContext>();
                var offerContext = scope.ServiceProvider.GetService<OfferContext>();
                try
                {
                    offerContext.Database.Migrate();
                    distributorManagmentContext.Database.Migrate();
                    productCatalogContext.Database.Migrate();
                    orderContext.Database.Migrate();
                    customerManagmentContext.Database.Migrate();
                    shoppingVanContext.Database.Migrate();
                }
                catch (Exception) { }
                SeedProductCatalogData(productCatalogContext);
                SeedCustomerCities(customerManagmentContext);
                SeedCustomerAreas(customerManagmentContext);
                SeedDistributorCities(distributorManagmentContext);
                SeedDistributorAreas(distributorManagmentContext);
            }
            return host;
        }

        public static void SeedProductCatalogData(ProductCatalogContext context)
        {
            if (context.Products.Count() < 10)
            {
                var random = new Randomizer();

                var testProductCategories = new Faker<ProductCategory>("ar")
                           .RuleFor(x => x.Id, x => Guid.NewGuid())
                           .RuleFor(x => x.Created, x => DateTime.UtcNow)
                           .RuleFor(x => x.CreatedBy, x => "Seeder")
                           .RuleFor(x => x.Name, x => x.Company.CompanyName())
                           .RuleFor(x => x.PhotoUrl, x => x.Image.PlaceholderUrl(200, 200, "Brimo Product Category", "#607d8b"));

                var testBrands = new Faker<Brand>("ar")
                       .RuleFor(x => x.Id, x => Guid.NewGuid())
                       .RuleFor(x => x.Created, x => DateTime.UtcNow)
                       .RuleFor(x => x.CreatedBy, x => "Seeder")
                       .RuleFor(x => x.Name, x => x.Commerce.Department())
                       .RuleFor(x => x.PhotoUrl, x => x.Image.PlaceholderUrl(200, 200, "Brimo Brand", "#eeeeee"));

                var testUnits = new Faker<Unit>("ar")
                       .RuleFor(x => x.Id, x => Guid.NewGuid())
                       .RuleFor(x => x.Created, x => DateTime.UtcNow)
                       .RuleFor(x => x.CreatedBy, x => "Seeder")
                       .RuleFor(x => x.ContentCount, x => x.Random.Int(1, 2000))
                       .RuleFor(x => x.Count, x => x.Random.Int(1, 2000))
                       .RuleFor(x => x.IsAvailable, x => x.Random.Bool(0.7f))
                       .RuleFor(x => x.Price, x => x.Random.Float(10, 1000))
                       .RuleFor(x => x.SellingPrice, x => x.Random.Float(100, 10000))
                       .RuleFor(x => x.Weight, x => x.Random.Float(1, 200))
                       .RuleFor(x => x.Name, x => x.Company.CompanyName());

                var brands = testBrands.Generate(100);
                var productCategories = testProductCategories.Generate(100);

                var testProducts = new Faker<Product>("ar")
                       .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                       .RuleFor(x => x.Created, x => DateTime.UtcNow)
                       .RuleFor(x => x.CreatedBy, x => "Seeder")
                       .RuleFor(x => x.PhotoUrl, x => x.Image.PlaceholderUrl(200, 200, "Brimo Product", "#9e9e9e"))
                       .RuleFor(x => x.AvailableToSell, x => x.Random.Bool(0.7f))
                       .RuleFor(x => x.Barcode, x => x.Commerce.Ean13())
                       .RuleFor(x => x.Units, x => testUnits.Generate(4))
                       .RuleFor(x => x.Brand, x => random.ListItem(brands))
                       .RuleFor(x => x.ProductCategory, x => random.ListItem(productCategories));

                var products = testProducts.Generate(2000);
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        public static void SeedCustomerCities(CustomerManagmentContext context)
        {
            if (!context.CustomersCities.Any())
            {
                var cities = new List<City>
                {
                    new City ("b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12","الشيخ زايد"),
                    new City ("1411e083-1975-40c1-a45e-1270d59a0cde","برج العرب"),
                    new City ("4d42f888-aa59-485d-ace9-5904c147e6d8","دَمَنْهور"),
                    new City ("73c4a2dd-36de-4376-bee4-45e4d3807e66","رَشيد"),
                    new City ("899dd7e2-8b12-41a4-8b6c-1f1352daab96","رأس البر"),
                };

                context.AddRange(cities);
                context.SaveChanges();
            }
        }

        public static void SeedCustomerAreas(CustomerManagmentContext context)
        {
            if (!context.CustomersAreas.Any())
            {
                var areas = new List<Area>
                {
                    new Area ("طامية", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "ee6688ee-03dd-4036-9c15-1e1eca655d11"),
                    new Area ("أطفيح", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "c4955067-104d-4361-8acd-be1fc214921e"),
                    new Area ("العياط", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "68cba8d6-2345-46f3-8b64-2a5c2c75596c"),

                    new Area ("قليوب", "1411e083-1975-40c1-a45e-1270d59a0cde", "ba7606f6-5b87-4fa8-b466-2ed87df17fe5"),
                    new Area ("القناطر الخيرية", "1411e083-1975-40c1-a45e-1270d59a0cde", "95e6f94f-87d9-4da7-95ae-566a3f4f573b"),
                    new Area ("الخانكة", "1411e083-1975-40c1-a45e-1270d59a0cde", "53094dc5-c4d0-4427-adc7-a28218157826"),

                    new Area ("طوخ", "4d42f888-aa59-485d-ace9-5904c147e6d8", "aaa61ce3-e74e-48e3-a9dc-40247292f906"),
                    new Area ("قها", "4d42f888-aa59-485d-ace9-5904c147e6d8", "6315e827-677f-4abc-91d4-ad519a6aa7a9"),
                    new Area ("الدلنجات", "4d42f888-aa59-485d-ace9-5904c147e6d8", "0720f640-c710-40ff-9178-40a45c2d86f5"),

                    new Area ("شبراخيت", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "0b06dda4-eeba-4003-904a-ee6c2b0fc705"),
                    new Area ("النوبارية الجديدة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "62396af2-3019-46c4-b500-7b2305f339a4"),
                    new Area ("الضبعة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "b0a1ee18-d9d9-4f9e-bb11-df2ba0ec2ddf"),

                    new Area ("سيوة", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "c84f8619-8965-4aeb-aaed-b87e9d5bfc61"),
                    new Area ("السرو", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "39f97a4f-221f-49bc-af60-d71ff0c4e836"),
                    new Area ("طلخا", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "2ac90763-52fc-406b-974e-c15e005cb442"),
                };

                context.AddRange(areas);
                context.SaveChanges();
            }
        }

        public static void SeedDistributorCities(DistributorManagmentContext context)
        {
            if (!context.DistributorsCities.Any())
            {
                var cities = new List<Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City>
                {
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City ("b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12","الشيخ زايد"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City ("1411e083-1975-40c1-a45e-1270d59a0cde","برج العرب"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City ("4d42f888-aa59-485d-ace9-5904c147e6d8","دَمَنْهور"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City ("73c4a2dd-36de-4376-bee4-45e4d3807e66","رَشيد"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.City ("899dd7e2-8b12-41a4-8b6c-1f1352daab96","رأس البر"),
                };

                context.AddRange(cities);
                context.SaveChanges();
            }
        }

        public static void SeedDistributorAreas(DistributorManagmentContext context)
        {
            if (!context.DistributorsAreas.Any())
            {
                var areas = new List<Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area>
                {
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("طامية", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "ee6688ee-03dd-4036-9c15-1e1eca655d11"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("أطفيح", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "c4955067-104d-4361-8acd-be1fc214921e"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("العياط", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", "68cba8d6-2345-46f3-8b64-2a5c2c75596c"),

                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("قليوب", "1411e083-1975-40c1-a45e-1270d59a0cde", "ba7606f6-5b87-4fa8-b466-2ed87df17fe5"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("القناطر الخيرية", "1411e083-1975-40c1-a45e-1270d59a0cde", "95e6f94f-87d9-4da7-95ae-566a3f4f573b"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("الخانكة", "1411e083-1975-40c1-a45e-1270d59a0cde", "53094dc5-c4d0-4427-adc7-a28218157826"),

                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("طوخ", "4d42f888-aa59-485d-ace9-5904c147e6d8", "aaa61ce3-e74e-48e3-a9dc-40247292f906"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("قها", "4d42f888-aa59-485d-ace9-5904c147e6d8", "6315e827-677f-4abc-91d4-ad519a6aa7a9"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("الدلنجات", "4d42f888-aa59-485d-ace9-5904c147e6d8", "0720f640-c710-40ff-9178-40a45c2d86f5"),

                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("شبراخيت", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "0b06dda4-eeba-4003-904a-ee6c2b0fc705"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("النوبارية الجديدة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "62396af2-3019-46c4-b500-7b2305f339a4"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("الضبعة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", "b0a1ee18-d9d9-4f9e-bb11-df2ba0ec2ddf"),

                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("سيوة", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "c84f8619-8965-4aeb-aaed-b87e9d5bfc61"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("السرو", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "39f97a4f-221f-49bc-af60-d71ff0c4e836"),
                    new Domain.DistributorManagment.AggregatesModel.DistributorAggregate.Area ("طلخا", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", "2ac90763-52fc-406b-974e-c15e005cb442"),
                };

                context.AddRange(areas);
                context.SaveChanges();
            }
        }
    }
}