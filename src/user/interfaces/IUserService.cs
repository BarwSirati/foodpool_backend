using FluentResults;
using FoodPool.user.dtos;

namespace FoodPool.user.interfaces;

public interface IUserService
{
    Task<Result<List<GetUserDto>>> GetAll();
    Task<Result<GetUserDto>> GetById(int id);
    Task<Result<GetUserDto>> GetCurrent(int id);
    Task<Result> Create(CreateUserDto createUserDto);
    Task<Result<GetUserDto>> Update(UpdateUserDto updateUserDto, int id);
    Task<Result> Delete(int id);
}