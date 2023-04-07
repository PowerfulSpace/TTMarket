using MediatR;

namespace TTMarket.Products.Application.Contracts.Messaging
{
    internal interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        
    }
}