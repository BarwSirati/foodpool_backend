using AutoMapper;
using FluentResults;
using FoodPool.order.dtos;
using FoodPool.order.entities;
using FoodPool.order.enums;
using FoodPool.order.interfaces;
using FoodPool.post.dtos;
using FoodPool.post.enums;
using FoodPool.post.interfaces;
using FoodPool.user.interfaces;

namespace FoodPool.order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository,
        IPostRepository postRepository, IUserService userService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _userService = userService;
    }

    public async Task<Result> Create(CreateOrderDto createOrderDto, int userId)
    {
        try
        {
            if (!_userRepository.ExistById(createOrderDto.UserId)) return Result.Fail(new Error("404"));
            if (!_postRepository.ExistById(createOrderDto.PostId)) return Result.Fail(new Error("404"));
            if (_orderRepository.ExistOrder(createOrderDto.PostId, createOrderDto.UserId))
                return Result.Fail(new Error("403"));
            var user = await _userRepository.GetById(createOrderDto.UserId);
            var post = await _postRepository.GetById(createOrderDto.PostId);
            var countOrder = await _orderRepository.GetCountOrderByPostId(createOrderDto.PostId);
            if (countOrder >= post.LimitOrder)
            {
                checkCount(post.Id);
                return Result.Fail(new Error("400"));
            }
            if (post?.User?.Id == userId) return Result.Fail(new Error("403"));
            await _userService.RemovePoint(createOrderDto.UserId);
            var order = _mapper.Map<Order>(createOrderDto);
            order.User = user;
            order.Post = post;
            _orderRepository.Insert(order);
            _orderRepository.Save();
            countOrder += 1;
            if (countOrder < post.LimitOrder) return Result.Ok();
            checkCount(post.Id);
            return Result.Fail(new Error("400"));
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }

    private void checkCount(int postId)
    {
        UpdatePost(new UpdatePostDto { PostStatus = PostStatus.Inactive }, postId);
    }

    private void UpdatePost(UpdatePostDto updatePostDto, int id)
    {
        if (!_postRepository.CheckStatus(id)) return;
        _postRepository.Update(updatePostDto, id);
        _postRepository.Save();
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
        var orders = await _orderRepository.GetByPostId(postId);
        if (orders.Any(order => order.Post.User.Id != userId))
        {
            return Result.Fail(new Error("403"));
        }

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
            if (findOrder.Value?.Post?.User.Id != userId) return Result.Fail(new Error("403"));
            if (findOrder.Value.Status.CompareTo(updateOrderDto.Status) == 1) return Result.Fail(new Error("403"));
            if (updateOrderDto.Status == OrderStatus.OrderDelivered)
                await _userService.AddPoint(userId);
            _orderRepository.Update(updateOrderDto, id);
            _orderRepository.Save();
            var order = await GetById(id);
            return Result.Ok(order.Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Fail(new Error("400"));
        }
    }

    public async Task<Result<GetOrderDto>> UpdateByOrderUser(UpdateOrderDto updateOrderDto, int id, int userId)
    {
        try
        {
            if (!_orderRepository.ExistById(id)) return Result.Fail(new Error("404"));
            var findOrder = await GetById(id);
            if (findOrder.Value.Status != OrderStatus.WaitingForConfirmation &&
                updateOrderDto.Status != OrderStatus.OrderCancelled) return Result.Fail(new Error("403"));
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