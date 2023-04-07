using System;
using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public sealed record UpdateProductCommand(Guid Id, ProductUpdateDto Product) : ICommand<Unit>;
}