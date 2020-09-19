using Core.Data.Models;
using Core.Elastic.Services;
using Infrastructure.Data.Models;
using Quartz;
using System.Linq;
using System.Threading.Tasks;

namespace Engine.Organization.Jobs
{
    public class ProductCacheJob : IJob
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly AdventureWorks2016Context _context;
        public ProductCacheJob(IElasticSearchService elasticSearchService, AdventureWorks2016Context context)
        {
            this._elasticSearchService = elasticSearchService;
            this._context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _elasticSearchService.CreateIndexAsync<VProductAndDescription>("yenijob", "yenijobalias");

            var list = _context.VProductAndDescription.ToList();
            _elasticSearchService.BulkIndex<VProductAndDescription>("yenijob", list);
        }
    }
}
