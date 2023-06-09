using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TTMarket.Products.Application.Contracts.Messaging;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductDetailDto>;
    
    public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDetailDto>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<ProductDetailDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindOneAsync(filterExpression: x => x.Id == request.Id,
                                                         cancellationToken: cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(request.Id.ToString());

            var productDto = _mapper.Map<ProductDetailDto>(product);

            return productDto;
        }
    }
}