using System;
using System.Collections.Generic;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Commands.Create
{
    public sealed class ProductCreateDto : IMapWith<Product>
    {
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
            profile.CreateMap<ProductCreateDto, Product>()
                   .ForMember(product => product.Name,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Name))
                   .ForMember(product => product.Price,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Price))
                   .ForMember(product => product.ShortDescription,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.ShortDescription))
                   .ForMember(product => product.Description,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Description))
                   .ForMember(product => product.MainImageUrl,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.MainImageUrl))
                   .ForMember(product => product.ImageUrls,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.ImageUrls))
                   .ForMember(product => product.Vendors,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Vendors))
                   .ForMember(product => product.MainInformation,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.MainInformation))
                   .ForMember(product => product.Specifications,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Specifications))
                   .ForMember(product => product.Tags,
                              opt => opt.MapFrom(productCreateDto => productCreateDto.Tags));
        }
    }
}