using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Data.Models;
using Core.Elastic.Services;
using Infrastructure.ElasticSearch.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElasticSearchService _elasticSearchService;

        public HomeController(ILogger<HomeController> logger, IElasticSearchService elasticSearchService)
        {
            _logger = logger;
            _elasticSearchService = elasticSearchService;
        }

        public IActionResult Index()
        {
            var model = new SearchModel();
            model.ProductList = new ElasticSearchBuilder("yenijob", _elasticSearchService)
           .SetSize(25)
           .SetFrom(0)
           .AddTermQuery("road", "name")
           .Build()
           .Execute<VProductAndDescription>();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
