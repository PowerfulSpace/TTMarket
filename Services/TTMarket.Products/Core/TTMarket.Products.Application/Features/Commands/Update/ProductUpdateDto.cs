using System;
using System.Collections.Generic;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Update
{
    public sealed class ProductUpdateDto : IMapWith<Product>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<Guid> Vendors { get; set; }
        public Dictionary<string, string> MainInformation { get; set; }
        public Dictionary<string, Dictionary<string,string>> Specifications { get; set; }
        public List<string> Tags { get; set; }

        void IMapWith<Product>.Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ProductUpdateDto, Product>()
                   .ForMember(product => product.CategoryId,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.CategoryId))
                   .ForMember(product => product.Name,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Name))
                   .ForMember(product => product.Price,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Price))
                   .ForMember(product => product.ShortDescription,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.ShortDescription))
                   .ForMember(product => product.Description,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Description))
                   .ForMember(product => product.MainImageUrl,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.MainImageUrl))
                   .ForMember(product => product.ImageUrls,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.ImageUrls))
                   .ForMember(product => product.Vendors,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Vendors))
                   .ForMember(product => product.MainInformation,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.MainInformation))
                   .ForMember(product => product.Specifications,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Specifications))
                   .ForMember(product => product.Tags,
                              opt => opt.MapFrom(productUpdateDto => productUpdateDto.Tags));
        }
    }
}