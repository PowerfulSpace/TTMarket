using System;

namespace TTMarket.Products.Domain.Common
{
    public abstract class Document : IDocument
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}