using AutoMapper;
using FluentResults;
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

    public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper,
        IStallRepository stallRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _stallRepository = stallRepository;
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

    public async Task<Result<GetPostDto>> Update(UpdatePostDto updatePostDto, int id)
    {
        try
        {
            if (!_postRepository.ExistById(id)) return Result.Fail(new Error("404"));
            var getPost = await _postRepository.GetById(id);
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
        return Result.Ok(posts.Select(post => _mapper.Map<GetPostDto>(post)).ToList());
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