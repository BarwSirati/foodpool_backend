using FoodPool.data;
using FoodPool.order.dtos;
using FoodPool.order.entities;
using FoodPool.order.enums;
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

    public async Task<Order> GetById(int id)
    {
        var order = await _dbContext.Order.Include(order => order.User).Include(order => order.Post)
            .Include(order => order.Post.User).Include(order => order.Post.Stall)
            .FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public bool ExistOrder(int postId, int userId)
    {
        return _dbContext.Order.Any(order => order.Post.Id == postId && order.User.Id == userId);
    }

    public async Task<int> GetCountOrderByPostId(int postId)
    {
        var count = await _dbContext.Order.Where(order => order.Post.Id == postId).CountAsync();
        return count;
    }


    public async Task<List<Order>> GetByPostId(int postId)
    {
        var orders = await _dbContext.Order.Include(order => order.User).Include(order => order.Post)
            .Include(order => order.Post.User).Include(order => order.Post.Stall)
            .Where(order => order.Post.Id == postId).OrderByDescending(order => order.Id)
            .ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetDeliveredOrderByUserId(int userId)
    {
        var orders = await _dbContext.Order.Include(order => order.User).Include(order => order.Post)
            .Include(order => order.Post.User).Include(order => order.Post.Stall)
            .Where(order => order.User!.Id == userId && order.Status == OrderStatus.OrderDelivered).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetByUserId(int userId)
    {
        var orders = await _dbContext.Order.Include(order => order.User).Include(order => order.Post)
            .Include(order => order.Post.User).Include(order => order.Post.Stall)
            .Where(o => o.User!.Id == userId).OrderByDescending(o => o.Id).ToListAsync();
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