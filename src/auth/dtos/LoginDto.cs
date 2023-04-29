using System.ComponentModel.DataAnnotations;

namespace FoodPool.auth.dtos;

public class LoginDto
{
    [Required] public string? Username { get; set; }

    [Required] public string? Password { get; set; }
}