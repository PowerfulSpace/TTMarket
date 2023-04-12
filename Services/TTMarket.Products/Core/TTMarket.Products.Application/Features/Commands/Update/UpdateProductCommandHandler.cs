using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTMarket.Products.Application.Contracts.Messaging;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public sealed record UpdateProductCommand(Guid Id, ProductUpdateDto Product) : ICommand<Unit>;
    
    public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Unit>
    {
        readonly IProductRepository _repository;
        readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
            => (_repository, _mapper) 
            = (repository, mapper);

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindOneAsync(filterExpression: x => x.Id == request.Id,
                                                         cancellationToken: cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(request.Id.ToString());

            _mapper.Map(request.Product, product);

            product.Updated = DateTime.Now;

            await _repository.ReplaceOneAsync(filterExpression: x => x.Id == product.Id,
                                              document: product,
                                              cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}