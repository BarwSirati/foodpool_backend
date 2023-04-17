using FluentResults;
using FoodPool.auth.dtos;

namespace FoodPool.auth.interfaces;

public interface IAuthService
{
    Task<Result<TokenDto>> Login(LoginDto loginDto);
}