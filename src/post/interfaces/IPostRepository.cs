using FoodPool.post.dtos;
using FoodPool.post.entities;

namespace FoodPool.post.interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetAll(int userId);

    bool CheckStatus(int postId);
    Task<List<Post>> GetByUserId(int userId);
    Task<Post> GetById(int id);
    bool ExistById(int id);
    void Update(UpdatePostDto updatePostDto, int id);
    void Insert(Post post);
    void Save();
}