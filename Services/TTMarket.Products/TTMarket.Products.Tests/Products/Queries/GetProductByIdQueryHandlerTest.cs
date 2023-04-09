using AutoMapper;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Queries.GetById;

namespace Services.TTMarket.Products.TTMarket.Products.Tests.Products.Queries
{
    public class GetProductByIdQueryHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;

        public GetProductByIdQueryHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetProductTest()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300");
            var command = new GetProductByIdQuery(id);
            var handler = new GetProductByIdQueryHandler(_mockRepo.Object, _mapper);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.ShouldBeOfType<ProductDetailDto>();
            result.Name.ShouldBe("Xiaomi 13");
        }
    }
}