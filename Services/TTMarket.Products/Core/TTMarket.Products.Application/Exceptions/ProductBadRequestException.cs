using TTMarket.Products.Domain.Exceptions;

namespace TTMarket.Products.Application.Exceptions
{
    public sealed class ProductBadRequestException : BadRequestException
    {
        public ProductBadRequestException(string message) 
            : base(message)
        {
        }
    }
}