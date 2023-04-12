using AutoMapper;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Application.Features.Queries.GetById;

namespace TTMarket.Products.Tests.Products.Queries
{
    public class GetProductByIdQueryHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;
        Guid _id;
        GetProductByIdQuery _command;
        readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTest()
         => (_mapper, _mockRepo, _id, _command, _handler)
         = (_mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            }).CreateMapper(),
            _mockRepo = MockProductRepository.GetProductRepository(),
            _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
            _command = new GetProductByIdQuery(_id),
            _handler = new GetProductByIdQueryHandler(_mockRepo.Object, _mapper));

        [Fact]
        public async Task Check_With_Valid_Id()
        {
            // Act
            var result = await _handler.Handle(_command, default);

            // Assert
            result.ShouldBeOfType<ProductDetailDto>();
            result.Name.ShouldBe("Xiaomi 13");
        }

        [Fact]
        public void Check_With_Invalid_Id()
        {
            // Arrange
            _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c400");
            _command = new GetProductByIdQuery(_id);
            var func = () => _handler.Handle(_command, default);

            // Act
            var result = Assert.ThrowsAsync<ProductNotFoundException>(func);

            // Assert
            result.Result.ShouldBeOfType<ProductNotFoundException>();
        }
    }
}