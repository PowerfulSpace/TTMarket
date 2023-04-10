
using FluentValidation.TestHelper;
using Moq;
using Newtonsoft.Json;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Commands.Create;

namespace TTMarket.Products.Tests.ValidatorsVerifiers
{
    public class CreateProductCommandValidatorTest
    {
        readonly CreateProductCommandValidator _validator;
        readonly Mock<IProductRepository> _mockRepo;
        readonly ProductCreateDto _model;
        public CreateProductCommandValidatorTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            _validator = new CreateProductCommandValidator(_mockRepo.Object);
            _model = GetDto();
        }

        [Fact]
        public async void Should_Not_Have_Error_When_Model_Is_Valid() 
        {
            // Arrange
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_Name() 
        {
            // Arrange
            _model.Name = null;
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Name() 
        {
            // Arrange
            _model.Name = "IPhone 14";
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Price() 
        {
            // Arrange
            _model.Price = 0;
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Price);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ShortDescription() 
        {
            // Arrange
            _model.ShortDescription = null;
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_ShortDescription() 
        {
            // Arrange
            _model.ShortDescription = new String('-', 301);
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ImageUrls() 
        {
            // Arrange
            _model.ImageUrls = null;
            var createProductCommand = new CreateProductCommand(_model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        private static ProductCreateDto GetDto()
        {
            var json = File.ReadAllText("../../../Mocks/Product.json");
            var product = JsonConvert.DeserializeObject<ProductCreateDto>(json);
            return product;
        }
    }
}