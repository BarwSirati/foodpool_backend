using FoodPool.post.entities;

namespace FoodPool.post.interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetAll();
    void Insert(Post post);
    void Save();
}