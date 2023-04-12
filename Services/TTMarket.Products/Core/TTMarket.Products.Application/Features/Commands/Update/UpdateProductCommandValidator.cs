using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TTMarket.Products.Application.Contracts.Persistence;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        readonly IProductRepository _repository;
        
        public UpdateProductCommandValidator(IProductRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must not be null")
                .MaximumLength(30).WithMessage("{PropertyName} must be fewer than 30 charachters");
            
            RuleFor(x => x.Product.Price)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must not be null")
                .GreaterThan(0).WithMessage("{PropertyName} must not be greater than 0");

            RuleFor(x => x.Product.ShortDescription)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must not be null")
                .MaximumLength(300).WithMessage("{PropertyName} must be fewer than 300 charachters");

            RuleFor(x => x.Product.ImageUrls)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must not be null");

            RuleFor(x => x)
                .MustAsync(CheckIsNameUnique)
                .WithName("Product.Name")
                .WithMessage("{PropertyName} with this name already exists");
        }

        async Task<bool> CheckIsNameUnique(UpdateProductCommand updateProductCommand,
                                           CancellationToken cancellationToken)
            => !await _repository.ExistsAsync(filterExpression: x => x.Id != updateProductCommand.Id && 
                                                                x.Name == updateProductCommand.Product.Name,
                                              cancellationToken: cancellationToken);
    }
}