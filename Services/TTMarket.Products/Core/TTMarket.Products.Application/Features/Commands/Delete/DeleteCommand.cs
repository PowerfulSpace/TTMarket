using System;
using MediatR;

namespace TTMarket.Products.Application.Features.Commands.Delete
{
    public record DeleteCommand(Guid Id) : IRequest<Unit>;
}