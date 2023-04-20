using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FoodPool.user.entities;
using FoodPool.stall.entities;
namespace FoodPool.post.entities;

public enum TypePost{
    AnyStall=0,
    OnlyThisStall=1
}

public enum PostStatus{
    Active=0,
    Inactive=1
}
public class Post
{
    [Key] public int Id { get; set; }
    public User? User { get; set; }
    public Stall? Stall { get; set; }
    public string? MenuName { get; set; }
    public string? Description { get; set; }
    [DefaultValue(0)]public PostStatus PostStatus { get; set; }
    public TypePost TypePost { get; set; }
    public int? LimitOrder { get; set; }
}