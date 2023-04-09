using AutoMapper;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Queries.GetAll;

namespace Services.TTMarket.Products.TTMarket.Products.Tests.Products.Queries
{
    public class GetAllProductsQueryHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;

        public GetAllProductsQueryHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Check_Get_All()
        {
            // Arrange
            var command = new GetAllProductsQuery();
            var handler = new GetAllProductsQueryHandler(_mockRepo.Object, _mapper);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.ShouldBeOfType<List<ProductDto>>();
            result.Count.ShouldBe(3);
        }
    }
}