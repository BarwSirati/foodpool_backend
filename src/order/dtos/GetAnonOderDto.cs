using FoodPool.post.dtos;

namespace FoodPool.order.dtos;

public class GetAnonOrderDto
{
    public int Id { get; set; }
    public GetPostDto? Post { get; set; }
    public string? MenuName { get; set; }
    public string? Note { get; set; }
}