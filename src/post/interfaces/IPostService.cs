using FluentResults;
using FoodPool.post.dtos;

namespace FoodPool.post.interfaces;

public interface IPostService
{
    Task<Result> Create(CreatePostDto createPostDto);
    Task<Result<GetPostDto>> Update(UpdatePostDto updatePostDto, int id, int userId);
    Task<Result<List<GetPostDto>>> GetAll(int userId);
    Task<Result<List<GetPostDto>>> GetByUserId(int userId);
    Task<Result<GetPostDto>> GetById(int id);
}