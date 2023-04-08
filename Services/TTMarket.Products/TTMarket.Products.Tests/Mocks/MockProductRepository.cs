using Moq;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;

namespace Services.TTMarket.Products.TTMarket.Products.Tests.Mocks
{
    internal static class MockProductRepository
    {
        internal static Mock<IProductRepository> GetProductRepository()
        {
            var products = new List<Product>()
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
                        "256b"
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
                        "256b"
                    }
                }
            };

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(products);
            mockRepo.Setup(r => r.FindByIdAsync(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
                                                CancellationToken.None)).ReturnsAsync(products[2]);

            return mockRepo;
        }
    }
}