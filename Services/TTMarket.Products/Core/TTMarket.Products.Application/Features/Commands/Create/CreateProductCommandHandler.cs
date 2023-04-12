using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    public sealed record CreateProductCommand(ProductCreateDto Product) : ICommand<Unit>;
    
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Unit>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);

            product.Created = DateTime.Now;
            product.Updated = null;

            await _repository.InsertOneAsync(document: product,
                                             cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}