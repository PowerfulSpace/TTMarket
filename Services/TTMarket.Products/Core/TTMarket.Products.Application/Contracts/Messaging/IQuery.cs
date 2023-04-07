using MediatR;

namespace TTMarket.Products.Application.Contracts.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}