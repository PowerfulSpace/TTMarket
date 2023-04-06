using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        readonly IProductRepository _repository;

        public UpdateCommandHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.Id, cancellationToken);

            if (!exists)
                throw new NotFoundException(request.Id.ToString());
            
            await _repository.UpdateAsync(request.Product, cancellationToken);

            return Unit.Value;
        }
    }
}