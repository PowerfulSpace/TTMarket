using MediatR;

namespace TTMarket.Catalogs.Application.Contracts.Messaging
{
    internal interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}