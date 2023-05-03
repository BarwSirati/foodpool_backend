using FoodPool.post.enums;
using FoodPool.stall.dtos;
using FoodPool.user.dtos;

namespace FoodPool.post.dtos;

public class GetPostDto
{
    public int Id { get; set; }
    public GetUserDto User { get; set; }

    public GetStallDto Stall { get; set; }

    public string? MenuName { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public PostStatus PostStatus { get; set; }

    public TypePost TypePost { get; set; }

    public int LimitOrder { get; set; }

    public int CountOrder { get; set; }
}