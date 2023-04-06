using MediatR;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public record UpdateCommand(Product Product) : IRequest<Unit>;
}