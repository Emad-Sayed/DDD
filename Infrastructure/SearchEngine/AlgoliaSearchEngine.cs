using Algolia.Search.Clients;
using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SearchEngine
{
    public class AlgoliaSearchEngine : ISearchEngine
    {
        private readonly SearchClient _client;
        private readonly AlgoliaSearchEngineConfigurations _algoliaSearchEngineConfigurations;

        public AlgoliaSearchEngine(AlgoliaSearchEngineConfigurations algoliaSearchEngineConfigurations)
        {
            _algoliaSearchEngineConfigurations = algoliaSearchEngineConfigurations;
            _client = new SearchClient(_algoliaSearchEngineConfigurations.ApplicationId, _algoliaSearchEngineConfigurations.APIKey);
        }

        public async Task AddEntity<T>(T entity, string indexName) where T : class
        {
            SearchIndex index = _client.InitIndex(indexName);
            await index.SaveObjectAsync(entity);
            ListProducts();

        }

        public  (List<Brand>, List<ProductCategory>, List<Product>) ListProducts()
        {
            SearchIndex indexToGetDataFrom = _client.InitIndex("dev_product2");

            var algoliaProductVMs = indexToGetDataFrom.Browse<AlgoliaProductVM>(new Algolia.Search.Models.Common.BrowseIndexQuery()).Take(100).ToList();

            var brandsFromAlgolia = algoliaProductVMs.Select(x => x.Brand).Distinct().ToList();
            var productCategoriesFromAlgolia = algoliaProductVMs.Select(x => x.Category).Distinct().ToList();

            var products = new List<Product>();
            var brands = new List<Brand>();
            var productCategories = new List<ProductCategory>();

            foreach (var brand in brandsFromAlgolia)
            {
                brands.Add(new Brand(brand));
            }
            foreach (var productCategory in productCategoriesFromAlgolia)
            {
                productCategories.Add(new ProductCategory(productCategory));
            }

            foreach (var productVM in algoliaProductVMs)
            {
                var brand = brands.FirstOrDefault(z => z.Name == productVM.Brand);
                var productCategory = productCategories.FirstOrDefault(z => z.Name == productVM.Category);

                var product = new Product(productVM.Name, productVM.Barcode, productVM.ImgUrl, productVM.AvailableToSell, brand.Id.ToString(), productCategory.Id.ToString());
                foreach (var unit in productVM.Units)
                {
                    product.AddUnitToProduct(unit.Name, 1, 1, unit.Price, unit.ConsumerPrice, 1, unit.IsAvailable);
                }
                products.Add(product);
            }

            return (brands, productCategories, products);
        }

        public async Task DeleteEntity(string objectId, string indexName)
        {
            SearchIndex index = _client.InitIndex(indexName);
            await index.DeleteObjectAsync(objectId);
        }

        public async Task UpdateEntity<T>(T entity, string indexName) where T : class
        {
            SearchIndex index = _client.InitIndex(indexName);
            await index.SaveObjectAsync(entity);
        }
    }
}
