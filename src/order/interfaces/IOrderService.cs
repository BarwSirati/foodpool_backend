using FluentResults;
using FoodPool.order.dtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.order.interfaces;

public interface IOrderService
{
    Task<Result> Create(CreateOrderDto createOrderDto);
    Task<Result<GetOrderDto>> GetOrderById(int id);
    Task<Result<List<GetOrderDto>>> GetOrderByPostId(int id);
}