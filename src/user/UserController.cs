using FoodPool.provider.interfaces;
using FoodPool.user.dtos;
using FoodPool.user.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.user;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IHttpContextProvider _contextProvider;

    public UserController(IUserService userService, IHttpContextProvider contextProvider)
    {
        _userService = userService;
        _contextProvider = contextProvider;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<GetUserDto>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users.Value);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> GetById(int id)
    {
        if (_contextProvider.GetCurrentUser() != id) return Unauthorized();
        var user = await _userService.GetById(id);
        if (user.Value is null) return NotFound();
        return Ok(user.Value);
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> GetCurrent()
    {
        var id = _contextProvider.GetCurrentUser();
        var user = await _userService.GetCurrent(id);
        if (!user.IsFailed) return Ok(user.Value);
        return user.Reasons[0].Message switch
        {
            "404" => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GetUserDto>> Update(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userService.Update(updateUserDto, id);
        if (!user.IsFailed) return Ok(user.Value);
        return user.Reasons[0].Message switch
        {
            "404" => NotFound(),
            "400" => BadRequest(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _userService.Delete(id);
        if (user.IsFailed) return BadRequest();
        return Ok();
    }
}