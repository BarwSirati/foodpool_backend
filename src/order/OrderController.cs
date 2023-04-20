using FoodPool.order.dtos;
using FoodPool.order.interfaces;
using FoodPool.provider.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPool.order;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextProvider _contextProvider;

    public OrderController(IOrderService orderService, IHttpContextProvider contextProvider)
    {
        _orderService = orderService;
        _contextProvider = contextProvider;
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<GetOrderDto>> GetById(int id)
    {
        var order = await _orderService.GetById(id);
        if (order.Value is null) return NotFound();
        return Ok(order.Value);
    }

    [HttpGet("user/{id:int}")]
    [Authorize]
    public async Task<ActionResult<List<GetOrderDto>>> GetByUserId(int id)
    {
        var orders = await _orderService.GetByUserId(id);
        if (orders.Value is null) return NotFound();
        return Ok(orders.Value);
    }

    [HttpGet("post/{id:int}")]
    [Authorize]
    public async Task<ActionResult<List<GetOrderDto>>> GetByPostId(int id)
    {
        var orders = await _orderService.GetByPostId(id);
        if (orders.Value is null) return NotFound();
        return Ok(orders.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
    {
        if (_contextProvider.GetCurrentUser() != createOrderDto.UserId) return Unauthorized();
        var order = await _orderService.Create(createOrderDto);
        if (order.IsFailed)
        {
            switch (order.Reasons[0].Message)
            {
                case "404":
                    return NotFound();
                case "400":
                    return BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok();
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<UpdateOrderDto>> UpdateById(UpdateOrderDto updateOrderDto, int id)
    {
        var order = await _orderService.UpdateById(updateOrderDto, id, _contextProvider.GetCurrentUser());
        return Ok(order.Value);
    }
}