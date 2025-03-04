using AutoMapper;
using Talabat.API.Dtos;
using Talabat.API.Dtos.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Product, ProductToReturnDTO>().ReverseMap();
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            //.ForMember(d => d.PictureUrl , O => O.MapFrom(p => $"{"https://localhost:7094"}/{p.PictureUrl}"))


            CreateMap<Employee, EmployeeToReturnDTO>()
                .ForMember(d => d.Department, O => O.MapFrom(s => s.Department.Name));


            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();

            CreateMap<AddressDTO, Address>();
        }
    }
}