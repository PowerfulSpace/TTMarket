using MediatR;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    public record CreateCommand(ProductCreateDto Product) : IRequest<Unit>;
}