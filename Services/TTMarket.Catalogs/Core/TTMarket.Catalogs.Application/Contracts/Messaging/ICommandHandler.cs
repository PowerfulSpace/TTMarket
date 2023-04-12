using MediatR;

namespace TTMarket.Catalogs.Application.Contracts.Messaging
{
    internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}