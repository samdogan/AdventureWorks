using Core.Elastic.Services;
using Core.ElasticSearch.Services;
using Infrastructure.ElasticSearch.Builders;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Elastic.Services
{
    public class ElasticSearchEngine : IElasticSearchEngine
    {
        private readonly string _indexName;
        private readonly int _size;
        private readonly int _from;
        private readonly IQueryContainer _queryContainer;
        private readonly IElasticSearchService elasticSearchService;

        public ElasticSearchEngine(ElasticSearchBuilder elasticSearchBuilder)
        {
            _indexName = elasticSearchBuilder.IndexName;
            _size = elasticSearchBuilder.Size;
            _from = elasticSearchBuilder.From;
            _queryContainer = elasticSearchBuilder.QueryContainer;
            elasticSearchService = elasticSearchBuilder._elasticSearchService;
        }

        #region IElasticSearchEngine Members
        public List<T> Execute<T>() where T : class
        {
            var response = elasticSearchService.Search<T>(new SearchRequest(_indexName)
            {
                Size = _size,
                From = _from,
                Query = (QueryContainer)_queryContainer
            });

            if (response.IsValid)
            {
                return response.Documents.ToList();
            }

            return null;
        }
        #endregion
    }
}
