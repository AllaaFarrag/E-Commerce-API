using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;

namespace E_Commerce.API.Helper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId , opt => opt.MapFrom(src => src.OrderItemProduct.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderItemProduct.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.OrderItemProduct.PictureUrl))
                .ForMember(dest => dest.PictureUrl , opt => opt.MapFrom<OrderItemResolver>());


            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethod , opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice , opt => opt.MapFrom(src => src.DeliveryMethod.Price));
        }
    }
}
