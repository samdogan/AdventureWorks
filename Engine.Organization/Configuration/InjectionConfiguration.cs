using Core.Elastic.Services;
using Engine.Organization.Factories;
using Engine.Organization.Jobs;
using Infrastructure.Data.Models;
using Infrastructure.Elastic;
using Infrastructure.Elastic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Collections.Specialized;

namespace Engine.Organization.Configuration
{
    public static class InjectionConfiguration
    {
        public  static void RegisterServices()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();

            var services = new ServiceCollection();


            services.AddElasticsearch(Configuration);

            services.AddDbContext<AdventureWorks2016Context>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value);
            });

            services.AddSingleton<ElasticHelper>();
            services.AddSingleton<IElasticSearchService, ElasticSearchService>();

            //services.AddSingleton<IJobFactory, JobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddHostedService<QuartzHostedService>();


           
        }
    }
}
