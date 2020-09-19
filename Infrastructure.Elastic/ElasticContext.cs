using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Elastic
{
    public static class ElasticContext
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("ElasticSearchOptions:ConnectionString:HostUrls").Value;

            var settings = new ConnectionSettings(new Uri(url));


            var client = new ElasticClient(settings);

            services.AddSingleton(client);
        }
    }
}
