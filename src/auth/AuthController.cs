using FoodPool.auth.dtos;
using FoodPool.auth.interfaces;
using FoodPool.user.dtos;
using FoodPool.user.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(CreateUserDto createUserDto)
    {
        var user = await _userService.Create(createUserDto);
        if (!user.IsFailed) return Ok();
        return user.Reasons[0].Message switch
        {
            "400" => BadRequest(),
            "409" => Conflict(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
    {
        var token = await _authService.Login(loginDto);
        if (!token.IsFailed) return Ok(token.Value);
        return token.Reasons[0].Message switch
        {
            "400" => BadRequest(),
            "403" => Forbid(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}