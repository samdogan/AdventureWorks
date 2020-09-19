using Core.Data.Services;

namespace Core.Data.Models
{
    public partial class VProductAndDescription : DataEntity
    {
        public string Name { get; set; }
        public string ProductModel { get; set; }
        public string CultureId { get; set; }
        public string Description { get; set; }
    }
}
