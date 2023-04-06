using System;
using MediatR;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public record UpdateCommand(Guid Id, Product Product) : IRequest<Unit>;
}