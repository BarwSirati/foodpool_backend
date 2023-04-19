using AutoMapper;
using FluentResults;
using FoodPool.order.dtos;
using FoodPool.order.entities;
using FoodPool.order.interfaces;
using FoodPool.user.interfaces;

namespace FoodPool.order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result> Create(CreateOrderDto createOrderDto)
    {
        if (!_userRepository.ExistById(createOrderDto.UserId)) return Result.Fail(new Error("404"));
        var user = await _userRepository.GetById(createOrderDto.UserId);
        var order = _mapper.Map<Order>(createOrderDto);
        order.User = user;
        _orderRepository.Insert(order);
        _orderRepository.Save();
        return Result.Ok();
    }

    public async Task<Result<GetOrderDto>> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderById(id);
        return _mapper.Map<GetOrderDto>(order);
    }
}