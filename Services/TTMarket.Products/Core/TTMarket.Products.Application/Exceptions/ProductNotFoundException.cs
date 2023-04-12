using TTMarket.Products.Domain.Exceptions;

namespace TTMarket.Products.Application.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string id) 
            : base($"Product with ID: {id} was not found")
        {
        }
    }
}