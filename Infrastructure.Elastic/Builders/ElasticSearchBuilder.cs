using Core.Elastic.Services;
using Infrastructure.Elastic.Services;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ElasticSearch.Builders
{
    public class ElasticSearchBuilder
    {
        internal string IndexName;
        internal int Size;
        internal int From;
        internal IQueryContainer QueryContainer;
        internal IElasticSearchService _elasticSearchService;

        public ElasticSearchBuilder(string indexName, IElasticSearchService elasticSearchService)
        {
            IndexName = indexName;
            this._elasticSearchService = elasticSearchService;

            QueryContainer = new QueryContainer();
        }

        public ElasticSearchBuilder SetSize(int size)
        {
            Size = size;

            return this;
        }

        public ElasticSearchBuilder SetFrom(int from)
        {
            From = from;

            return this;
        }

        public ElasticSearchBuilder AddTermQuery(string term, string field)
        {
            QueryContainer.Term = new TermQuery()
            {
                Field = field,
                Value = term
            };

            return this;
        }

        public ElasticSearchEngine Build()
        {
            return new ElasticSearchEngine(this);
        }
    }
}
