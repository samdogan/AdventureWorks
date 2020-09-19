using Core.Data.Models;
using System.Collections.Generic;

namespace Web.Models
{
    public class SearchModel
    {
        public string ProductName { get; set; }
        public List<VProductAndDescription> ProductList { get; set; }
    }
}
