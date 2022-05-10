using AutoMapper;
using Hire.Core.Domain.Orders;
using Hire.Core.Domain.Rentals;
using Hire.Services.Models;
using Hire.WebApi.Controllers;

namespace Hire.WebApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleEntity, Vehicle>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString().ToUpperInvariant()))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(VehiclesController.GetVehicleById), new { vehicleId = src.Id })));

            CreateMap<OrderEntity, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString().ToUpperInvariant()))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(OrdersController.GetOrderById), new { orderId = src.Id })));
        }
    }
}
