using FoodPool.data;
using FoodPool.order.entities;
using FoodPool.order.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.order;

public class OrderRepository : IOrderRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public OrderRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _dbContext.Order.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == id);
        return order!;
    }

    public void Insert(Order order)
    {
        _dbContext.Order.Add(order);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}