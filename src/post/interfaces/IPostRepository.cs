using FoodPool.post.entities;

namespace FoodPool.post.interfaces;

public interface IPostRepository
{
    void Insert(Post post);
    void Save();
}