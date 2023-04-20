using FoodPool.order.entities;

namespace FoodPool.order.interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderById(int id);

    Task<List<Order>> GetOrderByPostId(int postId);
    void Insert(Order order);
    void Save();
}