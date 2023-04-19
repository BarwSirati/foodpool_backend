using FoodPool.order.entities;

namespace FoodPool.order.interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderById(int id);
    void Insert(Order order);
    void Save();
}