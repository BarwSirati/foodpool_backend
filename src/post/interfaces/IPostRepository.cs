using FoodPool.post.entities;

namespace FoodPool.post.interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetAll();

    Task<List<Post>> GetByUserId(int userId);
    Task<Post> GetById(int id);

    void Insert(Post post);
    void Save();
}