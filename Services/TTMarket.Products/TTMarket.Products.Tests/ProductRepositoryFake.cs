using System.Linq.Expressions;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace Services.TTMarket.Products.TTMarket.Products.Tests
{
    public class ProductRepositoryFake : IProductRepository
    {
        List<Product> _products = new List<Product>()
        {
            new Product() 
            {
                Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c201"),
                Created = DateTime.Now,
                Updated = null,
                Name = "IPhone",
                Price = 2023,
                ShortDescription = "ShortDescription IPhone",
                Description = "Description IPhone",
                MainImageUrl = "http://fake.com/images/MainIphoneImage.jpg",
                ImageUrls = new List<string>()
                {
                    "http://fake.com/images/MainIphoneImage.jpg",
                    "http://fake.com/images/SecondIphoneImage.jpg"
                },
                Vendors = new List<Guid>()
                {
                    new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c202"),
                    new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c203")
                },
                MainInformation = new Dictionary<string, string>()
                {
                    { "Created", "2023" },
                    { "Updated", "2024" }
                },
                Specifications = new Dictionary<string, Dictionary<string, string>>()
                {
                    
                }
            }
        };
        public IQueryable<Product> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public bool CheckNameUnique(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckNameWhenUpdateUniqueAsync(Guid id, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Exists(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FilterBy(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<Product, bool>> filterExpression, Expression<Func<Product, TProjected>> projectionExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Product FindById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Product FindOne(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindOneAsync(Expression<Func<Product, bool>> filterExpression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(ICollection<Product> documents, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(ICollection<Product> documents, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(Product document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(Product document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void ReplaceOne(Product document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync(Product document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}