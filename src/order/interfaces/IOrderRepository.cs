using FoodPool.order.entities;

namespace FoodPool.order.interfaces;

public interface IOrderRepository
{
    Task<Order> GetById(int id);
    Task<List<Order>> GetByPostId(int postId);
    Task<List<Order>> GetByUserId(int userId);
    void Insert(Order order);
    void Save();
}