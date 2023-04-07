using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = TTMarket.Products.Application.Exceptions.ValidationException;

namespace TTMarket.Products.Application.Behaviors
{
    internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request,
                                                                            RequestHandlerDelegate<TResponse> next,
                                                                            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            
            var errorsDictionary = _validators
                .Select(x => x.ValidateAsync(context).Result)
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
                throw new ValidationException(errorsDictionary);

            return await next();
        }
    }
}