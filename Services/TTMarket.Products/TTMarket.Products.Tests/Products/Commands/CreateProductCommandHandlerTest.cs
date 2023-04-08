using AutoMapper;
using MediatR;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Commands.Create;
using TTMarket.Products.Application.Features.Queries.GetAll;

namespace TTMarket.Products.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;

        public CreateProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreatProductTest()
        {
            var createHandler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);
            var productCreateDto = GetDto();
            var resultOfCreat = await createHandler.Handle(new CreateProductCommand(productCreateDto),
                                              CancellationToken.None);

            var getAllhandler = new GetAllProductsQueryHandler(_mockRepo.Object, _mapper);
            var resultOfGetAll = await getAllhandler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            resultOfCreat.ShouldBeOfType<Unit>();
            resultOfGetAll.Count.ShouldBe(4);
        }
        
        private ProductCreateDto GetDto()
            => new ProductCreateDto()
                {
                    CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c401"),
                    Name = "POCO X5 Pro",
                    Price = 1199,
                    ShortDescription = "ShortDescription POCO X5 Pro",
                    Description = "Description POCO X5 Pro",
                    MainImageUrl = "http://fake.com/images/MainPOCOX5ProImage.jpg",
                    ImageUrls = new List<string>()
                    {
                        "http://fake.com/images/MainPOCOX5ProImage.jpg",
                        "http://fake.com/images/SecondPOCOX5ProImage.jpg"
                    },
                    Vendors = new List<Guid>()
                    {
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c402"),
                        new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c403")
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
                                { "Version of Operation System", "Android 12  (MIUI 14)" }
                            }
                        },
                        {
                            "Processor", 
                            new Dictionary<string, string>()
                            {
                                { "Platform", "Qualcomm Snapdragon" },
                                { "Processor", "Qualcomm Snapdragon 778G" },
                            }
                        }
                    },
                    Tags = new List<string>()
                    {
                        "POCO",
                        "256Gb"
                    }
                };
    }
}