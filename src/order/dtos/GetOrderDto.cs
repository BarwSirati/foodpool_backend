using FoodPool.order.enums;
using FoodPool.post.dtos;
using FoodPool.user.dtos;

namespace FoodPool.order.dtos;

public class GetOrderDto
{
    public int Id { get; set; }
    public GetPostDto? Post { get; set; }
    public GetUserDto? User { get; set; }
    public string? MenuName { get; set; }
    public OrderStatus Status { get; set; }
    public string? Note { get; set; }
}