using System.Collections.Generic;

namespace Core.ElasticSearch.Services
{
    public interface IElasticSearchEngine
    {
        List<T> Execute<T>() where T : class;
    }
}
