using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<ProductDto>>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetAllQueryHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<IEnumerable<ProductDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
    }
}