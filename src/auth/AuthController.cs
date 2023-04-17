using FoodPool.auth.dtos;
using FoodPool.auth.interfaces;
using FoodPool.user.dtos;
using FoodPool.user.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        if (user.IsFailed)
        {
            switch (user.Reasons[0].Message)
            {
                case "400":
                    return BadRequest();
                case "409":
                    return Conflict();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
    {
        var token = await _authService.Login(loginDto);
        if (token.IsFailed)
        {
            switch (token.Reasons[0].Message)
            {
                case "400":
                    return BadRequest();
                case "403":
                    return Forbid();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok(token.Value);
    }
}