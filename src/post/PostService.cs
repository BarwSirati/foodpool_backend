using AutoMapper;
using FluentResults;
using FoodPool.order.entities;
using FoodPool.order.interfaces;
using FoodPool.post.dtos;
using FoodPool.post.entities;
using FoodPool.post.enums;
using FoodPool.post.interfaces;
using FoodPool.stall.interfaces;
using FoodPool.user.interfaces;


namespace FoodPool.post;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStallRepository _stallRepository;
    private readonly IOrderRepository _orderRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper,
        IStallRepository stallRepository, IOrderRepository orderRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _stallRepository = stallRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result> Create(CreatePostDto createPostDto)
    {
        try
        {
            if (!_userRepository.ExistById(createPostDto.UserId)) return Result.Fail(new Error("404"));
            if (!_stallRepository.ExistById(createPostDto.UserId)) return Result.Fail(new Error("404"));
            var user = await _userRepository.GetById(createPostDto.UserId);
            var stall = await _stallRepository.GetById(createPostDto.StallId);
            var post = _mapper.Map<Post>(createPostDto);
            post.User = user;
            post.Stall = stall;
            _postRepository.Insert(post);
            _postRepository.Save();
            return Result.Ok();
        }
        catch (Exception)
        {
            return new Error("400");
        }
    }

    public async Task<Result<GetPostDto>> Update(UpdatePostDto updatePostDto, int id, int userId)
    {
        try
        {
            if (!_postRepository.ExistById(id)) return Result.Fail(new Error("404"));
            var getPost = await _postRepository.GetById(id);
            if (getPost.User.Id != userId) return Result.Fail(new Error("403"));
            updatePostDto.PostStatus = getPost.PostStatus;
            if (updatePostDto.PostStatus != PostStatus.Active) return Result.Fail(new Error("404"));
            updatePostDto.PostStatus = PostStatus.Inactive;
            _postRepository.Update(updatePostDto, id);
            _postRepository.Save();
            var post = await GetById(id);
            return Result.Ok(post.Value);
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }


    public async Task<Result<List<GetPostDto>>> GetAll(int userId)
    {
        var posts = await _postRepository.GetAll(userId);
        var postList = posts.Select(post => _mapper.Map<GetPostDto>(post)).ToList();
        var returnPost = new List<GetPostDto>();
        foreach (var p in postList.Where(p => !_orderRepository.ExistOrder(p.Id, userId)))
        {
            var count = await _orderRepository.GetCountOrderByPostId(p.Id);
            p.CountOrder = count;
            returnPost.Add(p);
        }

        return Result.Ok(returnPost);
    }

    public async Task<Result<GetPostDto>> GetById(int id)
    {
        var post = await _postRepository.GetById(id);
        return Result.Ok(_mapper.Map<GetPostDto>(post));
    }

    public async Task<Result<List<GetPostDto>>> GetByUserId(int userId)
    {
        var posts = await _postRepository.GetByUserId(userId);
        return Result.Ok(posts.Select(post => _mapper.Map<GetPostDto>(post)).ToList());
    }
}