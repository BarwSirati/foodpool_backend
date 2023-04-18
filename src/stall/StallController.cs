using FoodPool.provider.interfaces;
using FoodPool.stall.dtos;
using FoodPool.stall.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

}
