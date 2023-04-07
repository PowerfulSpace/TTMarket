using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductDetailDto>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public GetByIdQueryHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<ProductDetailDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.Id, cancellationToken);

            if (product is null)
                throw new NotFoundException(request.Id.ToString());

            var productDto = _mapper.Map<ProductDetailDto>(product);
            
            return productDto;
        }
    }
}