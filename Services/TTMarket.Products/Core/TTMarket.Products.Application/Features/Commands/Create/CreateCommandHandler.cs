using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public CreateCommandHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);
            
            await _repository.InsertOneAsync(product, cancellationToken);
            
            return Unit.Value;
        }
    }
}