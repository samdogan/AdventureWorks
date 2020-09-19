
using Core.ElasticSearch.Dto;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Elastic.Services
{
    public interface IElasticSearchService
    {
        Task<IndexResponseDto> CreateIndexAsync<T>(string indexName, string aliasName) where T : class;

        IndexResponseDto Index<T>(string indexName, T document) where T : class;
        IndexResponseDto BulkIndex<T>(string indexName, List<T> document) where T : class;
        SearchResponseDto<T> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
