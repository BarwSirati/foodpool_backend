using FoodPool.order.entities;
using FoodPool.order.dtos;

namespace FoodPool.order.interfaces;

public interface IOrderRepository
{
    Task<Order> GetById(int id);

    Task<int> GetCountOrderByPostId(int postId);
    Task<List<Order>> GetByPostId(int postId);
    Task<List<Order>> GetDeliveredOrderByUserId(int postId);
    Task<List<Order>> GetByUserId(int userId);
    bool ExistById(int id);
    void Update(UpdateOrderDto updateOrderDto, int id);
    void Insert(Order order);
    void Save();
}