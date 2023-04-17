using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

    [DefaultValue(0)] public int Point { get; set; }

    [StringLength(10)]
    [MaxLength(10)]
    [MinLength(10)]
    public string? Tel { get; set; }

    public string? Line { get; set; }
}