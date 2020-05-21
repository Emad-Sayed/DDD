using Algolia.Search.Clients;
using Domain.Base.Entity;
using Domain.Common.Interfaces;
using System;
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
