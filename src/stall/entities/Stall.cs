using System.ComponentModel.DataAnnotations;

namespace FoodPool.stall.entities;

public class Stall
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
}
