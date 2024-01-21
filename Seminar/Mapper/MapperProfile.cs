using AutoMapper;
using Seminar.Dto;
using Seminar.Models;

namespace Seminar.Mapper
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
