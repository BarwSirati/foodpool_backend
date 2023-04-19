using System.ComponentModel.DataAnnotations;

namespace FoodPool.order.dtos;

public class CreateOrderDto
{
    [Required] public int PostId { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string? MenuName { get; set; }
    [Required] public string? Location { get; set; }
    [Required] public string? Note { get; set; }
}