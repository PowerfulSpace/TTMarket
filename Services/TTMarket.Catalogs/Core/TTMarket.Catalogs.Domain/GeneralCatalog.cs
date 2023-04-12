using System.Collections.Generic;
using TTMarket.Catalogs.Domain.Common;

namespace TTMarket.Catalogs.Domain
{
    public class GeneralCatalog : BaseEntity
    {
        public string Name { get; set; }
        public List<BaseCatalog> BaseCatalog { get; set; }
    }
}