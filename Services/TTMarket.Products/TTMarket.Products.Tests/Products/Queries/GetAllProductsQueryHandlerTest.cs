using System.Reflection;
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
        public async Task GetProductsTest()
        {
            var handler = new GetAllProductsQueryHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<ProductDto>>();
            result.Count.ShouldBe(3);
        }
    }
}