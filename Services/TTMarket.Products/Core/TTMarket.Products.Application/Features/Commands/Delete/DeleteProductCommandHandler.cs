using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Commands.Delete
{
    public sealed record DeleteProductCommand(Guid Id) : ICommand<Unit>;
    
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Unit>
    {
        readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(filterExpression: x => x.Id == request.Id,
                                                       cancellationToken: cancellationToken);

            if (!exists)
                throw new ProductNotFoundException(request.Id.ToString());

            await _repository.DeleteOneAsync(filterExpression: x => x.Id == request.Id,
                                             cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}