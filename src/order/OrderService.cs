using AutoMapper;
using FluentResults;
using FoodPool.order.dtos;
using FoodPool.order.entities;
using FoodPool.order.enums;
using FoodPool.order.interfaces;
using FoodPool.post.interfaces;
using FoodPool.user.interfaces;

namespace FoodPool.order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<Result> Create(CreateOrderDto createOrderDto)
    {
        try
        {
            if (!_userRepository.ExistById(createOrderDto.UserId))
                return Result.Fail(new Error("404"));
            var user = await _userRepository.GetById(createOrderDto.UserId);
            var post = await _postRepository.GetById(createOrderDto.PostId);
            var order = _mapper.Map<Order>(createOrderDto);
            order.User = user;
            order.Post = post;
            _orderRepository.Insert(order);
            _orderRepository.Save();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }


    public async Task<Result<GetOrderDto>> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<Result<List<GetOrderDto>>> GetByUserId(int userId)
    {
        if (!_userRepository.ExistById(userId)) return Result.Fail(new Error("404"));
        var orders = await _orderRepository.GetByUserId(userId);
        return Result.Ok(orders.Select(order => _mapper.Map<GetOrderDto>(order)).ToList());
    }

    public async Task<Result<List<GetOrderDto>>> GetByPostId(int postId, int userId)
    {
        if (!_postRepository.ExistById(postId)) return Result.Fail(new Error("404"));
        var findOrder = await GetById(postId);
        if (findOrder.Value.Post.User.Id != userId) return Result.Fail(new Error("403"));
        var orders = await _orderRepository.GetByPostId(postId);
        return Result.Ok(orders.Select(order => _mapper.Map<GetOrderDto>(order)).ToList());
    }

    public async Task<Result<List<GetAnonOrderDto>>> GetAnonOrderByPostId(int postId)
    {
        var findOrder = await GetById(postId);
        var orders = await _orderRepository.GetByPostId(postId);
        return Result.Ok(orders.Select(order => _mapper.Map<GetAnonOrderDto>(order)).ToList());
    }

    public async Task<Result<List<GetOrderDto>>> GetDeliveredOrderByUserId(int userId)
    {
        var orders = await _orderRepository.GetDeliveredOrderByUserId(userId);
        return Result.Ok(orders.Select(order => _mapper.Map<GetOrderDto>(order)).ToList());
    }

    public async Task<Result<GetOrderDto>> UpdateByPostUser(UpdateOrderDto updateOrderDto, int id, int userId)
    {
        try
        {
            if (!_orderRepository.ExistById(id)) return Result.Fail(new Error("404"));
            var findOrder = await GetById(id);
            if (findOrder.Value.User!.Id != userId) return Result.Fail(new Error("403"));
            if (findOrder.Value.Status.CompareTo(updateOrderDto.Status) == 1) return Result.Fail(new Error("403"));
            _orderRepository.Update(updateOrderDto, id);
            _orderRepository.Save();
            var order = await GetById(id);
            return Result.Ok(order.Value);
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }

    public async Task<Result<GetOrderDto>> UpdateByOrderUser(UpdateOrderDto updateOrderDto, int id, int userId)
    {
        try
        {
            if (!_orderRepository.ExistById(id)) return Result.Fail(new Error("404"));
            var findOrder = await GetById(id);
            if (findOrder.Value.Status != OrderStatus.WaitingForConfirmation && updateOrderDto.Status != OrderStatus.OrderCancelled) return Result.Fail(new Error("403"));
            _orderRepository.Update(updateOrderDto, id);
            _orderRepository.Save();
            var order = await GetById(id);
            return Result.Ok(order.Value);
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }
}