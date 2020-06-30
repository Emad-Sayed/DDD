using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.CustomerManagment;
using Persistence.DistributorManagment;
using Persistence.OrderManagment;
using Persistence.ProductCatalog;
using Persistence.ShoppingVan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                try
                {
                    productCatalogContext.Database.Migrate();
                    orderContext.Database.Migrate();
                    customerManagmentContext.Database.Migrate();
                    shoppingVanContext.Database.Migrate();
                    distributorManagmentContext.Database.Migrate();
                }
                catch (Exception) { }
                SeedBrands(productCatalogContext);
                SeedProductCategories(productCatalogContext);
                SeedProducts(productCatalogContext);
                SeedCities(customerManagmentContext);
                SeedAreas(customerManagmentContext);
            }
            return host;
        }

        public static void SeedBrands(ProductCatalogContext context)
        {
            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
            {
                new Brand(  "كوسونز بيور", new Guid("8752f3e1-d4d2-4eb0-b616-4a88f915a21b") ),
                new Brand(  "لافو", new Guid("73ca8334-3d0c-4cf7-a0b0-048f657203ae") ),
                new Brand(  "ابو عوف", new Guid("a03f13c1-0f7e-443d-8296-e2660348171a") ),
            };
                context.AddRange(brands);
                context.SaveChanges();
            }
        }

        public static void SeedProductCategories(ProductCatalogContext context)
        {
            if (!context.ProductCategories.Any())
            {
                var productCategories = new List<ProductCategory>
            {
                new ProductCategory("مشروبات", new Guid("f4e3ec20-72c8-4e59-9d2b-34cee989e108") ),
                new ProductCategory("مأكولات", new Guid("0309710f-d381-4229-adc0-ede1e7933932") ),
                new ProductCategory("مكسرات", new Guid("9a750ed2-bfa3-4081-a18b-94c5df872f57") ),
            };
                context.AddRange(productCategories);
                context.SaveChanges();
            }
        }

        public static void SeedProducts(ProductCatalogContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
            {
                new Product("كريم استحمام بخلاصة التوت والفاوانيا من لو بوتي مارسيليه - 250 مل","DSOFOSIDF43","photo.png",true, "a03f13c1-0f7e-443d-8296-e2660348171a","0309710f-d381-4229-adc0-ede1e7933932",new Guid("25c5aa39-d1f9-44f9-890c-365465cb2a5e")),
                new Product("قهوة كلاسيك من جراند كافيه، 300 جم","KLSDFKJF233","photo.png",true, "8752f3e1-d4d2-4eb0-b616-4a88f915a21b","0309710f-d381-4229-adc0-ede1e7933932",new Guid("052216e4-25f6-4a0d-93b6-c792dc9cbd1b")),
                new Product("منظف سائل مركز للأغراض المنزلية برائحة التفاح من لافو - 750 مل","SDKFLURO454","photo.png",true, "8752f3e1-d4d2-4eb0-b616-4a88f915a21b","0309710f-d381-4229-adc0-ede1e7933932",new Guid("d04c2462-82a1-40a7-8234-6967b942b835")),
            };
                context.AddRange(products);
                context.SaveChanges();
            }
        }

        public static void SeedCities(CustomerManagmentContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City ("الشيخ زايد", new Guid("b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12")),
                    new City ("برج العرب", new Guid("1411e083-1975-40c1-a45e-1270d59a0cde")),
                    new City ("دَمَنْهور", new Guid("4d42f888-aa59-485d-ace9-5904c147e6d8")),
                    new City ("رَشيد", new Guid("73c4a2dd-36de-4376-bee4-45e4d3807e66")),
                    new City ("رأس البر", new Guid("899dd7e2-8b12-41a4-8b6c-1f1352daab96")),
                };

                context.AddRange(cities);
                context.SaveChanges();
            }
        }

        public static void SeedAreas(CustomerManagmentContext context)
        {
            if (!context.Areas.Any())
            {
                var cities = new List<Area>
                {
                    new Area ("طامية", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", new Guid("ee6688ee-03dd-4036-9c15-1e1eca655d11")),
                    new Area ("أطفيح", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", new Guid("c4955067-104d-4361-8acd-be1fc214921e")),
                    new Area ("العياط", "b74f5c89-cd7d-4e5e-9179-99b4c1e1ab12", new Guid("68cba8d6-2345-46f3-8b64-2a5c2c75596c")),

                    new Area ("قليوب", "1411e083-1975-40c1-a45e-1270d59a0cde", new Guid("ba7606f6-5b87-4fa8-b466-2ed87df17fe5")),
                    new Area ("القناطر الخيرية", "1411e083-1975-40c1-a45e-1270d59a0cde", new Guid("95e6f94f-87d9-4da7-95ae-566a3f4f573b")),
                    new Area ("الخانكة", "1411e083-1975-40c1-a45e-1270d59a0cde", new Guid("53094dc5-c4d0-4427-adc7-a28218157826")),

                    new Area ("طوخ", "4d42f888-aa59-485d-ace9-5904c147e6d8", new Guid("aaa61ce3-e74e-48e3-a9dc-40247292f906")),
                    new Area ("قها", "4d42f888-aa59-485d-ace9-5904c147e6d8", new Guid("6315e827-677f-4abc-91d4-ad519a6aa7a9")),
                    new Area ("الدلنجات", "4d42f888-aa59-485d-ace9-5904c147e6d8", new Guid("0720f640-c710-40ff-9178-40a45c2d86f5")),

                    new Area ("شبراخيت", "73c4a2dd-36de-4376-bee4-45e4d3807e66", new Guid("0b06dda4-eeba-4003-904a-ee6c2b0fc705")),
                    new Area ("النوبارية الجديدة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", new Guid("62396af2-3019-46c4-b500-7b2305f339a4")),
                    new Area ("الضبعة", "73c4a2dd-36de-4376-bee4-45e4d3807e66", new Guid("b0a1ee18-d9d9-4f9e-bb11-df2ba0ec2ddf")),

                    new Area ("سيوة", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", new Guid("c84f8619-8965-4aeb-aaed-b87e9d5bfc61")),
                    new Area ("السرو", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", new Guid("39f97a4f-221f-49bc-af60-d71ff0c4e836")),
                    new Area ("طلخا", "899dd7e2-8b12-41a4-8b6c-1f1352daab96", new Guid("2ac90763-52fc-406b-974e-c15e005cb442")),

                };

                context.AddRange(cities);
                context.SaveChanges();
            }
        }
    }
}
