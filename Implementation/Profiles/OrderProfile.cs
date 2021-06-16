using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.UserInfo, opt => opt.MapFrom(order => order.User.FirstName + " " + order.User.LastName))
                .ForMember(dto => dto.Status, opt => opt.MapFrom(order => order.OrderStatus.ToString()))
                .ForMember(dto => dto.PaymentMethod, opt => opt.MapFrom(order => order.PaymentMethod.ToString()))
                .ForMember(dto => dto.OrderLines, opt => opt.MapFrom(order => order.OrderLines.Select(ol => new OrderLineDto
                {
                    Id = ol.Id,
                    ProductId = ol.ProductId,
                    Name = ol.Name,
                    Price = ol.Price,
                    Quantity = ol.Quantity
                })))
                .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(order => order.OrderLines.Sum(ol => ol.Price * ol.Quantity)));
            CreateMap<OrderDto, Order>()
                .ForMember(order => order.OrderLines, opt => opt.Ignore());
        }
    }
}
