using AutoMapper;
using MediatR;
using Moq;
using Newtonsoft.Json;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using Shouldly;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Commands.Create;

namespace TTMarket.Products.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        readonly IMapper _mapper;
        readonly Mock<IProductRepository> _mockRepo;
        readonly ProductCreateDto _model;
        readonly CreateProductCommand _command;
        readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IProductRepository).Assembly));
            });
            _mapper = mapperConfig.CreateMapper();
            _model = GetDto();
            _command = new CreateProductCommand(_model);
            _handler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Check_With_Valid_Model()
        {           
            // Act
            var result = await _handler.Handle(_command, default);
            
            // Assert
            result.ShouldBeOfType<Unit>();
        }
        
        private static ProductCreateDto GetDto()
        {
            var json = File.ReadAllText("../../../Mocks/Product.json");
            var product = JsonConvert.DeserializeObject<ProductCreateDto>(json);
            return product;
        }
    }
}