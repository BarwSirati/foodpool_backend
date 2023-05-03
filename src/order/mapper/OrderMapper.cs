using AutoMapper;
using FoodPool.order.dtos;
using FoodPool.order.entities;

namespace FoodPool.order.mapper;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<Order, GetOrderDto>();
        CreateMap<Order, GetAnonOrderDto>();
    }
}