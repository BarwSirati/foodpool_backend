using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentResults;
using FoodPool.auth.dtos;
using FoodPool.auth.interfaces;
using FoodPool.user.entities;
using FoodPool.user.interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FoodPool.auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<Result<TokenDto>> Login(LoginDto loginDto)
    {
        if (!_userRepository.Exist(loginDto.Username!)) return Result.Fail(new Error("404"));
        var user = await _userRepository.GetByUsername(loginDto.Username!);
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) return Result.Fail(new Error("403"));
        var token = CreateToken(user);
        var tokenDto = new TokenDto { Token = token };
        return Result.Ok(tokenDto);
    }

    private string CreateToken(User user)
    {
        var claim = new List<Claim>
        {
            new Claim("userId", user.Id.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        if (credential == null) throw new ArgumentNullException(nameof(credential));
        var token = new JwtSecurityToken(claims: claim, expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credential, issuer: _configuration["issuer"], audience: _configuration["audience"]);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}