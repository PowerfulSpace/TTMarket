using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Product>>
    {
        readonly IProductRepository _repository;

        public GetAllQueryHandler(IProductRepository repository)
            => _repository = repository;

        public async Task<IEnumerable<Product>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync(cancellationToken);

            return data;
        }
    }
}