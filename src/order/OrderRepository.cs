using FoodPool.data;
using FoodPool.order.entities;
using FoodPool.order.interfaces;
using Microsoft.EntityFrameworkCore;
using FoodPool.order.dtos;

namespace FoodPool.order;

public class OrderRepository : IOrderRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public OrderRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetById(int id)
    {
        var order = await _dbContext.Order.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == id);
        return order!;
    }

    public async Task<List<Order>> GetByPostId(int postId)
    {
        var orders = await _dbContext.Order.Include(o => o.User).Where(o => o.PostId == postId).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetByUserId(int userId)
    {
        var orders = await _dbContext.Order.Include(o => o.User).Where(o => o.User!.Id == userId).ToListAsync();
        return orders;
    }

    public bool ExistById(int id)
    {
        return _dbContext.Order.Any(order => order.Id == id);
    }

    public void Update(UpdateOrderDto updateOrderDto, int id)
    {
        var order = GetById(id).Result;
        order.Status = updateOrderDto.Status;
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