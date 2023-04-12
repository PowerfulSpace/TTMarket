using AutoMapper;

namespace TTMarket.Catalogs.Application.Contracts.Mapping
{
    internal interface IMapWith<T>
    {
        void Mapping(Profile profile)
            => profile.CreateMap(typeof(T), GetType());
    }
}