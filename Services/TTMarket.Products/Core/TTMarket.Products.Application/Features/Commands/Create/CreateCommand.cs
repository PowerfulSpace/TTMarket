using MediatR;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    public record CreateCommand(Product Product) : IRequest<Unit>;
}