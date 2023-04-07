using System;
using TTMarket.Products.Application.Contracts.Mapping;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Features.Queries.GetAll
{
    public sealed class ProductDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ShortDescription { get; set; }
        public string MainImageUrl { get; set; }

        void IMapWith<Product>.Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                   .ForMember(productDto => productDto.Id,
                              opt => opt.MapFrom(product => product.Id))
                   .ForMember(productDto => productDto.Name,
                              opt => opt.MapFrom(product => product.Name))
                   .ForMember(productDto => productDto.Price,
                              opt => opt.MapFrom(product => product.Price))
                   .ForMember(productDto => productDto.ShortDescription,
                              opt => opt.MapFrom(product => product.ShortDescription))
                   .ForMember(productDto => productDto.MainImageUrl,
                              opt => opt.MapFrom(product => product.MainImageUrl));
        }
    }
}