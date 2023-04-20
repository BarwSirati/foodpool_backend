using System.ComponentModel.DataAnnotations;

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
    public int PostStatus { get; set; }
    
    [Required]
    public int TypePost { get; set; }
    
    [Required]
    public int? LimitOrder { get; set; }
    
}