using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TTMarket.Products.Application.Contracts.Messaging;
using TTMarket.Products.Application.Contracts.Persistence;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    public sealed record GetAllProductsQuery : IQuery<IEnumerable<ProductDto>>;
    internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
    }
}