using System.Linq.Expressions;
using Moq;
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

        static List<Product> GetProducts()
            => new List<Product>()
            {
                new Product() 
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100"),
                    CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c101"),
                    Created = DateTime.Now,
                    Updated = null,
                    Name = "IPhone 14",
                    Price = 2022,
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
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c102"),
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c103")
                    },
                    MainInformation = new Dictionary<string, string>()
                    {
                        { "Release Date", "2022" }
                    },
                    Specifications = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { 
                            "Main", 
                            new Dictionary<string, string>()
                            {
                                { "Type", "Smartphone" },
                                { "Operation System", "Apple iOS" },
                                { "Version of Operation System", "iOS 16" }
                            }
                        },
                        {
                            "Processor", 
                            new Dictionary<string, string>()
                            {
                                { "Platform", "Apple A" },
                                { "Processor", "Apple A15 Bionic" },
                            }
                        }
                    },
                    Tags = new List<string>()
                    {
                        "Iphone",
                        "128Gb"
                    }
                },
                new Product() 
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c201"),
                    Created = DateTime.Now,
                    Updated = null,
                    Name = "Samsung Galaxy S23",
                    Price = 2022,
                    ShortDescription = "ShortDescription Samsung",
                    Description = "Description Samsung",
                    MainImageUrl = "http://fake.com/images/MainSamsungImage.jpg",
                    ImageUrls = new List<string>()
                    {
                        "http://fake.com/images/MainSamsungImage.jpg",
                        "http://fake.com/images/SecondSamsungImage.jpg"
                    },
                    Vendors = new List<Guid>()
                    {
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c202"),
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c203")
                    },
                    MainInformation = new Dictionary<string, string>()
                    {
                        { "Release Date", "2023" }
                    },
                    Specifications = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { 
                            "Main", 
                            new Dictionary<string, string>()
                            {
                                { "Type", "Smartphone" },
                                { "Operation System", "Android" },
                                { "Version of Operation System", "Android 13  (One UI 5.1)" }
                            }
                        },
                        {
                            "Processor", 
                            new Dictionary<string, string>()
                            {
                                { "Platform", "Qualcomm Snapdragon" },
                                { "Processor", "Qualcomm Snapdragon 8 Gen2 SM8550" },
                            }
                        }
                    },
                    Tags = new List<string>()
                    {
                        "Samsung",
                        "256Gb"
                    }
                },
                new Product() 
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
                    CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c301"),
                    Created = DateTime.Now,
                    Updated = null,
                    Name = "Xiaomi 13",
                    Price = 2022,
                    ShortDescription = "ShortDescription Xiaomi",
                    Description = "Description Xiaomi",
                    MainImageUrl = "http://fake.com/images/MainXiaomiImage.jpg",
                    ImageUrls = new List<string>()
                    {
                        "http://fake.com/images/MainXiaomiImage.jpg",
                        "http://fake.com/images/SecondXiaomiImage.jpg"
                    },
                    Vendors = new List<Guid>()
                    {
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c302"),
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c303")
                    },
                    MainInformation = new Dictionary<string, string>()
                    {
                        { "Release Date", "2022" }
                    },
                    Specifications = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { 
                            "Main", 
                            new Dictionary<string, string>()
                            {
                                { "Type", "Smartphone" },
                                { "Operation System", "Android" },
                                { "Version of Operation System", "Android 13  (MIUI 14)" }
                            }
                        },
                        {
                            "Processor", 
                            new Dictionary<string, string>()
                            {
                                { "Platform", "Qualcomm Snapdragon" },
                                { "Processor", "Qualcomm Snapdragon 8 Gen2 SM8550" },
                            }
                        }
                    },
                    Tags = new List<string>()
                    {
                        "Xiaomi",
                        "256Gb"
                    }
                }
            };
    }
}