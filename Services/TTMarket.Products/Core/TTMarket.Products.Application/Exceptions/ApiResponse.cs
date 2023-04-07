using System.Collections.Generic;

namespace TTMarket.Products.Application.Exceptions
{
    public sealed class ApiResponse
    {
        public string title { get; set; }
        public int status { get; set; }
        public string detail { get; set; }
        public IReadOnlyDictionary<string, string[]> errors { get; set; }
    }
}