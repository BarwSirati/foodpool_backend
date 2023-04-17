using FoodPool.auth.dtos;
using FoodPool.auth.interfaces;
using FoodPool.user.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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

        return Ok();
    }
}