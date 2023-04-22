using System.ComponentModel.DataAnnotations;
using FoodPool.post.enums;

namespace FoodPool.post.dtos;

public class CreatePostDto
{
    [Required] public int UserId { get; set; }

    [Required] public int StallId { get; set; }

    [Required] public string? MenuName { get; set; }

    public string? Description { get; set; }

    [Required] public TypePost TypePost { get; set; }

    [Required] public string? Location { get; set; }

    [Required] public int LimitOrder { get; set; }
}