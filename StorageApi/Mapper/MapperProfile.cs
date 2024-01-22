using AutoMapper;
using StorageApi.Dto;
using StorageApi.Models;

namespace StorageApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Storage, StorageDto>().ReverseMap();
        }
    }
}
