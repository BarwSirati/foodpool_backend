using FluentResults;
using FoodPool.post.dtos;

namespace FoodPool.post.interfaces;

public interface IPostService
{
    Task<Result> Create(CreatePostDto createPostDto);

    Task<Result<List<GetPostDto>>> GetAll();
    Task<Result<GetPostDto>> GetById(int id);

}