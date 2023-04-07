using System.Collections.Generic;
using TTMarket.Products.Application.Contracts.Messaging;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    public sealed record GetAllProductsQuery : IQuery<IEnumerable<ProductDto>>;
}