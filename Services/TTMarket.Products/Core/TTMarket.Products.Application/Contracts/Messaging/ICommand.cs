using MediatR;

namespace TTMarket.Products.Application.Contracts.Messaging
{
    internal interface ICommand<out TResponse> : IRequest<TResponse>
    {        
    }
}