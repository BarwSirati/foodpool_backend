using System.ComponentModel.DataAnnotations;
using FoodPool.user.entities;

namespace FoodPool.order.entities;

public enum Status
{
    WaitingForConfirmation,
    HeadingToCafeteria,
    HeadingToTheDestination,
    OrderDelivered,
    OrderCancelled
}

public class Order
{
    [Key] public int Id { get; set; }
    // public int postId { get; set; }
    public User? User { get; set; }
    public string? MenuName { get; set; }
    public Status Status { get; set; }
    public string? Location { get; set; }
    public string? Note { get; set; }
}