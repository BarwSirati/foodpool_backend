using FoodPool.order.dtos;
using FoodPool.order.entities;

namespace FoodPool.order.interfaces;

public interface IOrderRepository
{
    Task<Order> GetById(int id);

    bool ExistOrder(int postId, int userId);

    Task<int> GetCountOrderByPostId(int postId);
    Task<List<Order>> GetByPostId(int postId);
    Task<List<Order>> GetDeliveredOrderByUserId(int postId);
    Task<List<Order>> GetByUserId(int userId);
    bool ExistById(int id);
    void Update(UpdateOrderDto updateOrderDto, int id);
    void Insert(Order order);
    void Save();
}