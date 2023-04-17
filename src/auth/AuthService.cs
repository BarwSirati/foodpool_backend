using FluentResults;
using FoodPool.auth.dtos;
using FoodPool.auth.interfaces;
using FoodPool.user.interfaces;

namespace FoodPool.auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<Result<TokenDto>> Login(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }
}