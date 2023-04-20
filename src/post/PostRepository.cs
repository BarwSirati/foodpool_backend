using FoodPool.data;
using FoodPool.post.entities;
using FoodPool.post.interfaces;

namespace FoodPool.post;

public class PostRepository :IPostRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public PostRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(Post post)
    {
       
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}