using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Core.Elastic.Services;
using Engine.Organization.Factories;
using Engine.Organization.Jobs;
using Infrastructure.Data.Models;
using Infrastructure.Elastic;
using Infrastructure.Elastic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace OrganizationWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private StdSchedulerFactory _schedulerFactory;
        private CancellationToken _stopppingToken;
        private IScheduler _scheduler;
        private ServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, CancellationToken _stopppingToken)
        {
            _logger = logger;
            this._stopppingToken = _stopppingToken;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartJobs();
            _stopppingToken = stoppingToken;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(55000, stoppingToken);
            }
            await _scheduler.Shutdown();
        }

        protected async Task StartJobs()
        {
            var services = new ServiceCollection();
            await RegisterServices(services);
        }

        public static async Task RegisterServices(ServiceCollection services)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();


            services.AddScoped<ProductCacheJob>();

            services.AddElasticsearch(Configuration);

            services.AddDbContext<AdventureWorks2016Context>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value);
            });

            services.AddSingleton<IElasticSearchService, ElasticSearchService>();
            services.AddSingleton<ElasticHelper>();

            var _serviceProvider = services.BuildServiceProvider();
            await ScheduleJob(_serviceProvider);
        }

        private static async Task ScheduleJob(IServiceProvider serviceProvider)
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);
            var sched = await factory.GetScheduler();
            sched.JobFactory = new JobFactory(serviceProvider);

            await sched.Start();
            var job = JobBuilder.Create<ProductCacheJob>()
                .WithIdentity("myJob", "group1")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
            .Build();
            await sched.ScheduleJob(job, trigger);
        }
    }
}

