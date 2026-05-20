using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class OrderUpdateRequestToOrderMappingProfile : Profile
    {
        public OrderUpdateRequestToOrderMappingProfile()
        {
            CreateMap<OrderUpdateRequest, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest._id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());
        }
    }
}
