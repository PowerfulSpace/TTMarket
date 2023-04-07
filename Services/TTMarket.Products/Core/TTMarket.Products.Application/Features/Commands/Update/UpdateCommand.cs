using System;
using MediatR;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public record UpdateCommand(Guid Id, ProductUpdateDto Product) : IRequest<Unit>;
}