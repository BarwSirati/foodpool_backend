using System.ComponentModel.DataAnnotations;

namespace FoodPool.post.dtos;

public class CreatePostDto
{
    [Required]
    public int? UserId { get; set; }
    
    [Required]
    public int? StallId { get; set; }
    
    [Required]
    public string? MenuName { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public int TypePost { get; set; }
    
    [Required]
    public int? LimitOrder { get; set; }
}