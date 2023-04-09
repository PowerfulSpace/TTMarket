using AutoMapper;
using MediatR;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Application.Features.Commands.Delete;

namespace TTMarket.Products.Tests.Products.Commands
{
    public class DeleteProductCommandHandlerTest
    {
        readonly Mock<IProductRepository> _mockRepo;

        public DeleteProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            });
        }

        [Fact]
        public async Task Check_With_Valid_Id()
        {
            // Arrange
            var command = new DeleteProductCommand(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"));
            var handler = new DeleteProductCommandHandler(_mockRepo.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Check_With_Invalid_Id()
        {
            // Arrange
            var command = new DeleteProductCommand(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c400"));
            var handler = new DeleteProductCommandHandler(_mockRepo.Object);
            var func = () => handler.Handle(command, default);

            // Act
            var result = Assert.ThrowsAsync<ProductNotFoundException>(func);

            // Assert
            result.Result.ShouldBeOfType<ProductNotFoundException>();
        }
    }
}