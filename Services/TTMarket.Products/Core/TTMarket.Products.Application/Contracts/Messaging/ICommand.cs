using MediatR;

namespace TTMarket.Products.Application.Contracts.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {        
    }
}