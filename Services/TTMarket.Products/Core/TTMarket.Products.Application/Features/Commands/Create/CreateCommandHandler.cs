using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        readonly IProductRepository _repository;

        public CreateCommandHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await _repository.InsertOneAsync(request.Product, cancellationToken);
            
            return Unit.Value;
        }
    }
}