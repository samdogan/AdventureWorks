using System.Collections.Generic;

namespace Core.ElasticSearch.Dto
{
    public class SearchResponseDto<T> : IndexResponseDto
    {
        public IEnumerable<T> Documents { get; set; }
    }
}
