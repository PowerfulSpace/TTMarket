using System.Linq.Expressions;
using Moq;
using Newtonsoft.Json;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace Services.TTMarket.Products.TTMarket.Products.Tests.Mocks
{
    internal static class MockProductRepository
    {
        internal static Mock<IProductRepository> GetProductRepository()
        {
            var products = GetProducts();

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync((CancellationToken cancellationToken) => 
                    { 
                        return products; 
                    });

            mockRepo.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Product, bool>>>(),
                                               It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Expression<Func<Product, bool>> expression,
                                   CancellationToken cancellationToken) => 
                    {
                        return products.Where(expression.Compile()).SingleOrDefault();
                    });

            mockRepo.Setup(x => x.InsertOneAsync(It.IsAny<Product>(),
                                                 It.IsAny<CancellationToken>()))
                    .Callback((Product product,
                               CancellationToken cancellationToken) => 
                    {
                       products.Add(product);
                    });

            mockRepo.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>(),
                                              It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Expression<Func<Product, bool>> expression,
                                   CancellationToken cancellationToken) => 
                    {
                        return products.Where(expression.Compile()).Any();
                    });

            mockRepo.Setup(x => x.DeleteOneAsync(It.IsAny<Expression<Func<Product, bool>>>(),
                                                 It.IsAny<CancellationToken>()))
                    .Callback((Expression<Func<Product, bool>> expression,
                               CancellationToken cancellationToken) => 
                    {
                        var productToRemove = products.Where(expression.Compile()).First();
                        products.Remove(productToRemove);
                    });

            mockRepo.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Product, bool>>>(),
                                               It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Expression<Func<Product, bool>> expression,
                                   CancellationToken cancellationToken) => 
                    { 
                        return products.Where(expression.Compile()).FirstOrDefault();
                    });

            return mockRepo;
        }

        private static List<Product> GetProducts()
        {
            var json = File.ReadAllText("../../../Mocks/ProductList.json");
            var product = JsonConvert.DeserializeObject<List<Product>>(json);
            return product;
        }
    }
}