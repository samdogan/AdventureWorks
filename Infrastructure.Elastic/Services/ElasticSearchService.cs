using Core.ElasticSearch.Dto;
using Core.Elastic.Services;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Elastic.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticClient _elasticClient;

        public ElasticSearchService(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        #region IElasticContext Members
        public async Task<IndexResponseDto> CreateIndexAsync<T>(string indexName, string aliasName) where T : class
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
             .Aliases(a => a.Alias(aliasName));

            var response = await _elasticClient.Indices.CreateAsync(createIndexDescriptor);

            return new IndexResponseDto()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public IndexResponseDto Index<T>(string indexName, T document) where T : class
        {
            var response = _elasticClient.Index(document, i => i
                           .Index(indexName));

            return new IndexResponseDto()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public IndexResponseDto BulkIndex<T>(string indexName, List<T> document) where T : class
        {
            var response = _elasticClient.IndexMany(document, indexName);

            return new IndexResponseDto()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public SearchResponseDto<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            var response = _elasticClient.Search<T>(searchRequest);

            return new SearchResponseDto<T>()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException,
                Documents = response.Documents
            };
        }

        #endregion
    }
}
