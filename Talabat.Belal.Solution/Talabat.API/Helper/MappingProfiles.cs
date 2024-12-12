using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;

namespace Talabat.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Product, ProductToReturnDTO>().ReverseMap();
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Brand , O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category , O => O.MapFrom(s => s.Category.Name));
        }
    }
}
