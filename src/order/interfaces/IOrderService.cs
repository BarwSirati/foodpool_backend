using FluentResults;
using FoodPool.order.dtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.order.interfaces;

public interface IOrderService
{
    Task<Result> Create(CreateOrderDto createOrderDto);
    Task<Result<GetOrderDto>> GetById(int id);
    Task<Result<List<GetOrderDto>>> GetByUserId(int userId);
    Task<Result<List<GetOrderDto>>> GetByPostId(int postId, int userId);
    Task<Result<List<GetOrderDto>>> GetDeliveredOrderByUserId(int postId);
    Task<Result<GetOrderDto>> UpdateByPostUser(UpdateOrderDto updateOrderDto, int id, int userId);
    Task<Result<GetOrderDto>> UpdateByOrderUser(UpdateOrderDto updateOrderDto, int id, int userId);
    
}