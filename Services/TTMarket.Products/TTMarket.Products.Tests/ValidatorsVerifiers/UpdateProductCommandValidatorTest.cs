using FluentValidation.TestHelper;
using Moq;
using Newtonsoft.Json;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Commands.Update;

namespace TTMarket.Products.Tests.ValidatorsVerifiers
{
    public class UpdateProductCommandValidatorTest
    {
        readonly UpdateProductCommandValidator _validator;
        readonly Mock<IProductRepository> _mockRepo;
        readonly ProductUpdateDto _model;
        readonly Guid _id;
        readonly UpdateProductCommand _command;
        public UpdateProductCommandValidatorTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            _validator = new UpdateProductCommandValidator(_mockRepo.Object);
            _model = GetDto();
            _id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            _command = new UpdateProductCommand(_id, _model);
        }

        [Fact]
        public async void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Not_Have_Error_When_Model_Is_Have_Same_Name()
        {
            // Arrange
            _model.Name = "Samsung Galaxy S23";

            // Act
            var result = await _validator.TestValidateAsync(_command);

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

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Name() 
        {
            // Arrange
            _model.Name = "IPhone 14";

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Price() 
        {
            // Arrange
            _model.Price = 0;

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Price);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ShortDescription() 
        {
            // Arrange
            _model.ShortDescription = null;

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_ShortDescription() 
        {
            // Arrange
            _model.ShortDescription = new String('-', 301);

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ImageUrls() 
        {
            // Arrange
            _model.ImageUrls = null;

            // Act
            var result = await _validator.TestValidateAsync(_command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        private static ProductUpdateDto GetDto()
        {
            var json = File.ReadAllText("../../../Mocks/Product.json");
            var product = JsonConvert.DeserializeObject<ProductUpdateDto>(json);
            return product;
        }
    }
}