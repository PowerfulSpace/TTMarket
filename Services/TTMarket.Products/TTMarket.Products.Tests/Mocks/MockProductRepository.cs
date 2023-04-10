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

            mockRepo.Setup(x => x.FindByIdAsync(It.IsAny<Guid>(),
                                                It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Guid id,
                                   CancellationToken cancellationToken) => 
                    {
                        return products.Where(x => x.Id == id).FirstOrDefault();
                    });

            mockRepo.Setup(x => x.InsertOneAsync(It.IsAny<Product>(),
                                                 It.IsAny<CancellationToken>()))
                    .Callback((Product product,
                               CancellationToken cancellationToken) => 
                    {
                       products.Add(product);
                    });

            mockRepo.Setup(x => x.ExistsAsync(It.IsAny<Guid>(),
                                              It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Guid id,
                                   CancellationToken cancellationToken) => 
                    {
                        return products.Where(x => x.Id == id).Any();
                    });

            mockRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>(),
                                                  It.IsAny<CancellationToken>()))
                    .Callback((Guid id,
                               CancellationToken cancellationToken) => 
                    {
                        var productToRemove = products.Where(x => x.Id == id).First();
                        products.Remove(productToRemove);
                    });

            mockRepo.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Product, bool>>>(),
                                               It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Expression<Func<Product, bool>> expression,
                                   CancellationToken cancellationToken) => 
                    { 
                        return products.Where(expression.Compile()).FirstOrDefault();
                    });

            mockRepo.Setup(x => x.CheckNameUniqueAsync(It.IsAny<string>(),
                                                       It.IsAny<CancellationToken>()))
                    .ReturnsAsync((string name,
                                   CancellationToken cancellationToken) => 
                    {
                        return products.Where(x => x.Name == name).Any();
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