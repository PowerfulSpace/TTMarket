using System;
using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;

namespace TTMarket.Products.Application.Features.Commands.Delete
{
    public sealed record DeleteProductCommand(Guid Id) : ICommand<Unit>;
}