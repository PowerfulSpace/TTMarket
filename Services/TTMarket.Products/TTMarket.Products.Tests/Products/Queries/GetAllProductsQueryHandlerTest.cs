using AutoMapper;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Queries.GetAll;

namespace TTMarket.Products.Tests.Products.Queries
{
    public class GetAllProductsQueryHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;
        readonly GetAllProductsQuery _command;
        readonly GetAllProductsQueryHandler _handler;

        public GetAllProductsQueryHandlerTest()
            => (_mapper, _mockRepo, _command, _handler)
            = (_mapper = new MapperConfiguration(cfg => 
               {
                   cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
               }).CreateMapper(),
               _mockRepo = MockProductRepository.GetProductRepository(),
               _command = new GetAllProductsQuery(),
               _handler = new GetAllProductsQueryHandler(_mockRepo.Object, _mapper));

        [Fact]
        public async Task Check_Get_All()
        {
            // Act
            var result = await _handler.Handle(_command, default);

            // Assert
            result.ShouldBeOfType<List<ProductDto>>();
            result.Count.ShouldBe(3);
        }
    }
}