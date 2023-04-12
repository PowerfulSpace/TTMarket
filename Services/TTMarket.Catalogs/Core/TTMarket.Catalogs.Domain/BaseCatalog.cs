using System.Collections.Generic;
using TTMarket.Catalogs.Domain.Common;

namespace TTMarket.Catalogs.Domain
{
    public class BaseCatalog : BaseEntity
    {
        public string Name { get; set; }
        public List<Catalog> Catalog { get; set; }
    }
}