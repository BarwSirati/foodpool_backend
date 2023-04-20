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

    public Task<Result> Create(CreateOrderDto createOrderDto)
    {
        try
        {
            if (!_userRepository.ExistById(createOrderDto.UserId)) return Task.FromResult(Result.Fail(new Error("404")));
            var user = _userRepository.GetById(createOrderDto.UserId);
            var order = _mapper.Map<Order>(createOrderDto);
            order.User = user.Result;
            _orderRepository.Insert(order);
            _orderRepository.Save();
            return Task.FromResult(Result.Ok());
        }
        catch (Exception)
        {
            return Task.FromResult(Result.Fail(new Error("400")));
        }
    }

    public async Task<Result<GetOrderDto>> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<Result<List<GetOrderDto>>> GetByUserId(int userId)
    {
        var orders = await _orderRepository.GetByUserId(userId);
        return Result.Ok(orders.Select(order =>_mapper.Map<GetOrderDto>(order)).ToList());
    }

    public async Task<Result<List<GetOrderDto>>> GetByPostId(int postId)
    {
        var orders = await _orderRepository.GetByPostId(postId);
        return Result.Ok(orders.Select(order => _mapper.Map<GetOrderDto>(order)).ToList());
    }
}