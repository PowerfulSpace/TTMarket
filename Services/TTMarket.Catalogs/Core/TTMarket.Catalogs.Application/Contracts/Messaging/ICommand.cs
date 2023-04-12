using MediatR;

namespace TTMarket.Catalogs.Application.Contracts.Messaging
{
    internal interface ICommand<out TResponse> : IRequest<TResponse>
    {        
    }
}