using System;
using Core.Data.Models;
using Core.Elastic.Services;
using Infrastructure.Elastic;
using Infrastructure.Elastic.Services;
using Infrastructure.ElasticSearch.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            IConfiguration Configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();


            services.AddElasticsearch(Configuration);            

            services.AddSingleton<IElasticSearchService, ElasticSearchService>();
            services.AddSingleton<ElasticHelper>();

            var _serviceProvider = services.BuildServiceProvider();

            var sample1Service = _serviceProvider.GetService<IElasticSearchService>();

            var elasticSearchEngine = new ElasticSearchBuilder("yenijob", sample1Service)
            .SetSize(5)
            .SetFrom(0)
            .AddTermQuery("bottom", "name")
            .Build()
            .Execute<VProductAndDescription>();

            elasticSearchEngine.ForEach(x =>
            Console.WriteLine($"Name = {x.Name} Description = {x.Description}"));


            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
