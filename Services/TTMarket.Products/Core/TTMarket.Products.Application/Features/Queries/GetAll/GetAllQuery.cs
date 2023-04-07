using System.Collections.Generic;
using MediatR;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    public record GetAllQuery : IRequest<IEnumerable<ProductDto>>;
}