using System.ComponentModel.DataAnnotations;
using FoodPool.order.enums;

namespace FoodPool.order.dtos;

public class UpdateOrderDto
{
    [EnumDataType(typeof(OrderStatus))]
    [Required]
    public OrderStatus Status { get; set; }
}