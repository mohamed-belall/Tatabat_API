using AutoMapper;
using Talabat.API.Dtos;
using Talabat.API.Dtos.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using OrderAddress = Talabat.Core.Entities.Order_Aggregate.Address;
using UserAddress = Talabat.Core.Entities.Identity.Address;
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

            CreateMap<AddressDTO, OrderAddress>();
            CreateMap<UserAddress, AddressDTO>();




            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.DeliveryMethod , O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost , O => O.MapFrom(s => s.DeliveryMethod.Cost))
                ;

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>()) 
                ;
                //.ForMember(d => d.PictureUrl, O => O.MapFrom(p => $"{"https://localhost:7094"}/{p.Product.PictureUrl}"))
        }
    }
}