using System;
using MediatR;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    public record GetByIdQuery(Guid Id) : IRequest<ProductDetailDto>;
}