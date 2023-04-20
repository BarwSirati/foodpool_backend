using System.ComponentModel.DataAnnotations;
using FoodPool.order.enums;
using FoodPool.user.entities;

namespace FoodPool.order.entities;

public class Order
{
    [Key] public int Id { get; set; }
    public int PostId { get; set; }
    public User User { get; set; }
    public string? MenuName { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.WaitingForConfirmation;
    public string? Location { get; set; }
    public string? Note { get; set; }
}