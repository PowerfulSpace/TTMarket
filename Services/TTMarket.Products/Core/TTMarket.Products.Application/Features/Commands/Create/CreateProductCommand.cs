using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    public sealed record CreateProductCommand(ProductCreateDto Product) : ICommand<Unit>;
}