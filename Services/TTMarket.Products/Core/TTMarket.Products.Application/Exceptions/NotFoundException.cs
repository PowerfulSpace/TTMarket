using System;

namespace TTMarket.Products.Application.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string key) 
            : base($"Product with ID:\"{key}\" was not found.")
        {
        }
    }
}