using System.ComponentModel.DataAnnotations;

namespace FoodPool.user.dtos;

public class CreateUserDto
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Lastname { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    [StringLength(10)]
    [MinLength(10)]
    [MaxLength(10)]
    public string? Tel { get; set; }

    [Required]
    public string? Line { get; set; }
}