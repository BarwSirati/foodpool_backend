using FoodPool.order.enums;
using FoodPool.user.dtos;
namespace FoodPool.order.dtos;

public class GetOrderDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public GetUserDto? User { get; set; }
    public string? MenuName { get; set; }
    public OrderStatus Status { get; set; }
    public string? Location { get; set; }
    public string? Note { get; set; }
}