using System;

namespace TTMarket.Catalogs.Domain.Common
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}