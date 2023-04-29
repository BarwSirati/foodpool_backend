using System.ComponentModel.DataAnnotations;
using FoodPool.post.enums;

namespace FoodPool.post.dtos;

public class UpdatePostDto
{

    [Required]
    public PostStatus PostStatus { get; set; }


}