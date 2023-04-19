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
    public async Task<ActionResult<GetOrderDto>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return Ok(order.Value);
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
}