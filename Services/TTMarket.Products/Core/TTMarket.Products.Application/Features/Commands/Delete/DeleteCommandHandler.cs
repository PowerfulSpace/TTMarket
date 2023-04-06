using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Commands.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
    {
        readonly IProductRepository _repository;

        public DeleteCommandHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.Id, cancellationToken);

            if (!exists)
                throw new NotFoundException(request.Id.ToString());

            await _repository.DeleteByIdAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}