using System.ComponentModel.DataAnnotations;

namespace FoodPool.stall.dtos;

public class CreateStallDto
{
    [Required]
    public string? Name { get; set; }
}