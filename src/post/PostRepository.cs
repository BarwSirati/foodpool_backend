using FoodPool.data;
using FoodPool.post.dtos;
using FoodPool.post.entities;
using FoodPool.post.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.post;

public class PostRepository : IPostRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public PostRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>> GetAll(int userId)
    {
        var posts = await _dbContext.Post.Include(o => o.User).Include(o => o.Stall).Where(o => o.User.Id != userId)
            .OrderByDescending(o => o.Id)
            .ToListAsync();
        return posts;
    }


    public async Task<List<Post>> GetByUserId(int userId)
    {
        var posts = await _dbContext.Post.Include(o => o.User).Include(o => o.Stall).ToListAsync();
        return posts;
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _dbContext.Post.Include(o => o.User).Include(o => o.Stall)
            .FirstOrDefaultAsync(o => o.Id == id);
        return post!;
    }


    public bool ExistById(int id)
    {
        return _dbContext.Post.Any(u => u.Id == id);
    }

    public void Update(UpdatePostDto updatePostDto, int id)
    {
        var post = GetById(id).Result;
        post.PostStatus = updatePostDto.PostStatus;
    }

    public void Insert(Post post)
    {
        _dbContext.Post.Add(post);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}