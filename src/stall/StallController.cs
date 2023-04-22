using FoodPool.provider.interfaces;
using FoodPool.stall.dtos;
using FoodPool.stall.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.stall;

[ApiController]
[Route("api/stall")]
public class StallController : ControllerBase
{
    private readonly IStallService _stallService;
    private readonly IHttpContextProvider _contextProvider;

    public StallController(IStallService stallService, IHttpContextProvider contextProvider)
    {
        _stallService = stallService;
        _contextProvider = contextProvider;
    }

    [HttpGet]
    public async Task<ActionResult<GetStallDto>> GetAll()
    {
        var stalls = await _stallService.GetAll();
        return Ok(stalls.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetStallDto>> GetById(int id)
    {
        var stall = await _stallService.GetById(id);
        if (stall.Value is null) return NotFound();
        return Ok(stall.Value);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create(CreateStallDto createStallDto)
    {
        var stall = await _stallService.Create(createStallDto);
        if (stall.IsFailed)
        {
            switch (stall.Reasons[0].Message)
            {
                case "400":
                    return BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetStallDto>> Update(UpdateStallDto updateStallDto, int id)
    {
        var stall = await _stallService.Update(updateStallDto, id);
        if (stall.IsFailed)
        {
            switch (stall.Reasons[0].Message)
            {
                case "404":
                    return NotFound();
                case "400":
                    return BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok(stall.Value);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        var stall = await _stallService.Delete(id);
        if (stall.IsFailed) return BadRequest();
        return Ok();
    }
}
