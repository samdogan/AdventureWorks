using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Elastic
{
    public class ElasticHelper
    {
        private readonly IConfiguration configuration;
        private static readonly Lazy<ElasticHelper> _Instance = new Lazy<ElasticHelper>(() => new ElasticHelper());

        public ElasticHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private ElasticHelper()
        {

        }

        public static ElasticHelper Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        #region Public Methods
        public ConnectionSettings GetConnectionSettings()
        {
            var connectionSettings = new ConnectionSettings(new Uri(configuration.GetSection("ElasticSearchOptions:ConnectionString:HostUrls").Value));

            return connectionSettings;
        }
        #endregion
    }
}
