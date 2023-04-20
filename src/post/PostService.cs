using FluentResults;
using FoodPool.post.dtos;
using FoodPool.post.interfaces;

namespace FoodPool.post;

public class PostService:IPostService
{
    public Task<Result> Create(CreatePostDto createPostDto)
    {
        throw new NotImplementedException();
    }
}