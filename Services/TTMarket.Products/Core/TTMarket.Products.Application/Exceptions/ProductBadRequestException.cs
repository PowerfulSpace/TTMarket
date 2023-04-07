using TTMarket.Products.Domain.Exceptions;

namespace TTMarket.Products.Application.Exceptions
{
    internal sealed class ProductBadRequestException : BadRequestException
    {
        public ProductBadRequestException(string message) 
            : base(message)
        {
        }
    }
}