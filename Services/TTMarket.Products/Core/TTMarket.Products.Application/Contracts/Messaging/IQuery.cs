using MediatR;

namespace TTMarket.Products.Application.Contracts.Messaging
{
    internal interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}