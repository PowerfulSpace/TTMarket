using MediatR;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Application.Features.Commands.Delete;

namespace TTMarket.Products.Tests.Products.Commands
{
    public class DeleteProductCommandHandlerTest
    {
        readonly Mock<IProductRepository> _mockRepo;
        Guid _id;
        DeleteProductCommand _command;
        readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTest()
            => (_mockRepo, _id, _command, _handler)
            = (_mockRepo = MockProductRepository.GetProductRepository(),
               _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
               _command = new DeleteProductCommand(_id),
               _handler = new DeleteProductCommandHandler(_mockRepo.Object));

        [Fact]
        public async Task Check_With_Valid_Id()
        {
            // Act
            var result = await _handler.Handle(_command, default);

            // Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Check_With_Invalid_Id()
        {
            // Arrange
            _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c400");
            _command = new DeleteProductCommand(_id);
            var func = () => _handler.Handle(_command, default);

            // Act
            var result = Assert.ThrowsAsync<ProductNotFoundException>(func);

            // Assert
            result.Result.ShouldBeOfType<ProductNotFoundException>();
        }
    }
}