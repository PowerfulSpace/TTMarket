using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Product>
    {
        readonly IProductRepository _repository;

        public GetByIdQueryHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<Product> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.Id, cancellationToken);

            if (product is null)
                throw new NotFoundException(request.Id.ToString());
            
            return product;
        }
    }
}