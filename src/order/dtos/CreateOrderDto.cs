using System.ComponentModel.DataAnnotations;

namespace FoodPool.order.dtos;

public class CreateOrderDto
{
    [Required] public int PostId { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string? MenuName { get; set; }
    public string? Note { get; set; }
}