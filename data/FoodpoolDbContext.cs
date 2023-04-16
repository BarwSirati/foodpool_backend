using Microsoft.EntityFrameworkCore;

namespace FoodPool.data;

public class FoodpoolDbContext : DbContext
{
    public FoodpoolDbContext(DbContextOptions<FoodpoolDbContext> options) : base(options)
    {
    }
}