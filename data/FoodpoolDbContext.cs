using FoodPool.user.entities;
using FoodPool.stall.entities;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.data;

public class FoodpoolDbContext : DbContext
{
    public FoodpoolDbContext(DbContextOptions<FoodpoolDbContext> options) : base(options)
    {
    }

    public DbSet<User> User => Set<User>();
    public DbSet<Stall> Stall => Set<Stall>();
}