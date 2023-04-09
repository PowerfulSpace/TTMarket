
using FluentValidation.TestHelper;
using Moq;
using Services.TTMarket.Products.TTMarket.Products.Tests.Mocks;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Application.Features.Commands.Create;

namespace TTMarket.Products.Tests.ValidatorsVerifiers
{
    public class CreateProductCommandValidatorTest
    {
        readonly CreateProductCommandValidator _validator;
        readonly Mock<IProductRepository> _mockRepo;
        public CreateProductCommandValidatorTest()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            _validator = new CreateProductCommandValidator(_mockRepo.Object);
        }

        [Fact]
        public async void Should_Not_Have_Error_When_Model_Is_Valid() 
        {
            // Arrange
            var model = GetDto();
            var createProductCommand = new CreateProductCommand(model);

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
            var model = GetDto();
            model.Name = null;
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Name() 
        {
            // Arrange
            var model = GetDto();
            model.Name = "IPhone 14";
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_Price() 
        {
            // Arrange
            var model = GetDto();
            model.Price = 0;
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ShortDescription() 
        {
            // Arrange
            var model = GetDto();
            model.ShortDescription = null;
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Invalid_ShortDescription() 
        {
            // Arrange
            var model = GetDto();
            model.ShortDescription = new String('-', 301);
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        [Fact]
        public async void Should_Have_Error_When_Model_Is_Null_ImageUrls() 
        {
            // Arrange
            var model = GetDto();
            model.ImageUrls = null;
            var createProductCommand = new CreateProductCommand(model);

            // Act
            var result = await _validator.TestValidateAsync(createProductCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.Price);
            result.ShouldNotHaveValidationErrorFor(x => x.Product.ShortDescription);
            result.ShouldHaveValidationErrorFor(x => x.Product.ImageUrls);
        }

        private ProductCreateDto GetDto()
            => new ProductCreateDto()
            {
                CategoryId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c401"),
                Name = "POCO X5 Pro",
                Price = 1199,
                ShortDescription = "ShortDescription POCO X5 Pro",
                Description = "Description POCO X5 Pro",
                MainImageUrl = "http://fake.com/images/MainPOCOX5ProImage.jpg",
                ImageUrls = new List<string>()
                {
                    "http://fake.com/images/MainPOCOX5ProImage.jpg",
                    "http://fake.com/images/SecondPOCOX5ProImage.jpg"
                },
                Vendors = new List<Guid>()
                {
                    new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c402"),
                    new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c403")
                },
                MainInformation = new Dictionary<string, string>()
                {
                    { "Release Date", "2023" }
                },
                Specifications = new Dictionary<string, Dictionary<string, string>>()
                {
                    { 
                        "Main", 
                        new Dictionary<string, string>()
                        {
                            { "Type", "Smartphone" },
                            { "Operation System", "Android" },
                            { "Version of Operation System", "Android 12  (MIUI 14)" }
                        }
                    },
                    {
                        "Processor", 
                        new Dictionary<string, string>()
                        {
                            { "Platform", "Qualcomm Snapdragon" },
                            { "Processor", "Qualcomm Snapdragon 778G" },
                        }
                    }
                },
                Tags = new List<string>()
                {
                    "POCO",
                    "256Gb"
                }
            };
    }
}