using FoodPool.order.entities;
using FoodPool.post.entities;
using FoodPool.stall.entities;
using FoodPool.user.entities;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.data;

public class FoodpoolDbContext : DbContext
{
    public FoodpoolDbContext(DbContextOptions<FoodpoolDbContext> options) : base(options)
    {
    }

    public DbSet<User> User => Set<User>();
    public DbSet<Stall> Stall => Set<Stall>();
    public DbSet<Order> Order => Set<Order>();

    public DbSet<Post> Post => Set<Post>();
}