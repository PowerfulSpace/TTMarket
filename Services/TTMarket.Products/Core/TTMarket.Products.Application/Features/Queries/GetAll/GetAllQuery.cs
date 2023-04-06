using System.Collections.Generic;
using MediatR;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    public record GetAllQuery : IRequest<IReadOnlyList<Product>>;
}