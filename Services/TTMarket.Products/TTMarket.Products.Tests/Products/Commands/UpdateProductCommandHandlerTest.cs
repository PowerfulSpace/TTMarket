using AutoMapper;
using MediatR;
using Moq;
using Newtonsoft.Json;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Application.Features.Commands.Update;

namespace TTMarket.Products.Tests.Products.Commands
{
    public class UpdateProductCommandHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;
        readonly ProductUpdateDto _model;
        Guid _id;
        UpdateProductCommand _command;
        readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTest()
            => (_mapper, _mockRepo, _model, _id, _command, _handler)
            = (_mapper = new MapperConfiguration(cfg => 
               {
                   cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
               }).CreateMapper(),
               _mockRepo = MockProductRepository.GetProductRepository(),
               _model = GetDto(),
               _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
               _command = new UpdateProductCommand(_id, _model),
               _handler = new UpdateProductCommandHandler(_mockRepo.Object, _mapper));

        [Fact]
        public async Task Check_With_Valid_Model()
        {
            // Act
            var result = await _handler.Handle(_command, default);
            
            // Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Check_With_Invalid_Id_Model()
        {
            // Arrange
            _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c400");
            _command = new UpdateProductCommand(_id, _model);
            var func = () => _handler.Handle(_command, default);
            
            // Act
            var result = Assert.ThrowsAsync<ProductNotFoundException>(func);

            // Assert
            result.Result.ShouldBeOfType<ProductNotFoundException>();
        }

        private static ProductUpdateDto GetDto()
        {
            var json = File.ReadAllText("../../../Mocks/Product.json");
            var product = JsonConvert.DeserializeObject<ProductUpdateDto>(json);
            return product;
        }
    }
}