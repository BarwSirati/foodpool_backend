using System.ComponentModel.DataAnnotations;
using FoodPool.user.enums;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.user.entities;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public int Point { get; set; } = 0;

    [StringLength(10)]
    [MaxLength(10)]
    [MinLength(10)]
    public string? Tel { get; set; }

    public string? Line { get; set; }

    public Role Role { get; set; } = Role.User;
}