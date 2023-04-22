using System.ComponentModel.DataAnnotations;
using FoodPool.order.enums;
using FoodPool.post.entities;
using FoodPool.user.entities;

namespace FoodPool.order.entities;

public class Order
{
    [Key] public int Id { get; set; }
    public Post? Post { get; set; }
    public User? User { get; set; }
    public string? MenuName { get; set; }
    [EnumDataType(typeof(OrderStatus))] public OrderStatus Status { get; set; } = OrderStatus.WaitingForConfirmation;
    public string? Note { get; set; }
}