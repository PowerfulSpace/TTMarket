using System;
using TTMarket.Products.Application.Contracts.Messaging;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductDetailDto>;
}