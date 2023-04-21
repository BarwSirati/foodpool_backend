using System.ComponentModel.DataAnnotations;
using FoodPool.post.enums;

namespace FoodPool.post.dtos;

public class UpdatePostDto
{
    [Required]
    public int User { get; set; }
    
    [Required]
    public int Stall { get; set; }
    
    [Required]
    public string? MenuName { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public PostStatus PostStatus { get; set; }
    
    [Required]
    public TypePost TypePost { get; set; }
    
    [Required]
    public int LimitOrder { get; set; }
    
}