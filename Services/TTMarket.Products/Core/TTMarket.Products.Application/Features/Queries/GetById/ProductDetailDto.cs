using System;
using System.Collections.Generic;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetById
{
    public sealed class ProductDetailDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
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
            profile.CreateMap<Product, ProductDetailDto>()
                   .ForMember(productDetailDto => productDetailDto.Id,
                              opt => opt.MapFrom(product => product.Id))
                   .ForMember(productDetailDto => productDetailDto.CategoryId,
                              opt => opt.MapFrom(product => product.CategoryId))
                   .ForMember(productDetailDto => productDetailDto.Name,
                              opt => opt.MapFrom(product => product.Name))
                   .ForMember(productDetailDto => productDetailDto.Price,
                              opt => opt.MapFrom(product => product.Price))
                   .ForMember(productDetailDto => productDetailDto.ShortDescription,
                              opt => opt.MapFrom(product => product.ShortDescription))
                   .ForMember(productDetailDto => productDetailDto.Description,
                              opt => opt.MapFrom(product => product.Description))
                   .ForMember(productDetailDto => productDetailDto.MainImageUrl,
                              opt => opt.MapFrom(product => product.MainImageUrl))
                   .ForMember(productDetailDto => productDetailDto.ImageUrls,
                              opt => opt.MapFrom(product => product.ImageUrls))
                   .ForMember(productDetailDto => productDetailDto.Vendors,
                              opt => opt.MapFrom(product => product.Vendors))
                   .ForMember(productDetailDto => productDetailDto.MainInformation,
                              opt => opt.MapFrom(product => product.MainInformation))
                   .ForMember(productDetailDto => productDetailDto.Specifications,
                              opt => opt.MapFrom(product => product.Specifications))
                   .ForMember(productDetailDto => productDetailDto.Tags,
                              opt => opt.MapFrom(product => product.Tags));
        }
    }
}