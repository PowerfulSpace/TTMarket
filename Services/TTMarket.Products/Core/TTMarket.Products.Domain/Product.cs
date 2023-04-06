using System;
using System.Collections.Generic;
using TTMarket.Products.Domain.Common;

namespace TTMarket.Products.Domain
{
    public class Product : Document
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<Guid> Vendors { get; set; }
        public Dictionary<string, string> MainInformation { get; set; }
        public Dictionary<string, Dictionary<string,string>> Specifications { get; set; }
        public List<string> Tags { get; set; }
    }
}