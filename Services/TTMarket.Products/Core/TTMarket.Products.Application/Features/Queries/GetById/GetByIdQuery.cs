using System;
using MediatR;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    public record GetByIdQuery(Guid Id) : IRequest<Product>;
}