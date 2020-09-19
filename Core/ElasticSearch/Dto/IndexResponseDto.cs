using System;

namespace Core.ElasticSearch.Dto
{
    public class IndexResponseDto
    {
        public bool IsValid { get; set; }
        public string StatusMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
