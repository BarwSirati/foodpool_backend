namespace FoodPool.user.dtos;

public class GetUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; set; }
    public int Point { get; set; }
    public string? Tel { get; set; }
    public string? Line { get; set; }
}