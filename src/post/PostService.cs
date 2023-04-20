using AutoMapper;
using FluentResults;
using FoodPool.post.dtos;
using FoodPool.post.entities;
using FoodPool.post.interfaces;
using FoodPool.user.interfaces;


namespace FoodPool.post;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;


    public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result> Create(CreatePostDto createPostDto)
    {
        var post = _mapper.Map<Post>(createPostDto);
        _postRepository.Insert(post);
        _postRepository.Save();
        return Result.Ok();
    }

    public async Task<Result<List<GetPostDto>>> GetAll()
    {
        var posts = await _postRepository.GetAll();
        return Result.Ok(posts.Select(post => _mapper.Map<GetPostDto>(post)).ToList());
    }

    public async Task<Result<GetPostDto>> GetById(int id)
    {
        var post = await _postRepository.GetById(id);
        return Result.Ok(_mapper.Map<GetPostDto>(post));
    }

    public async Task<Result<List<GetPostDto>>> GetByUserId(int userId){
        var posts = await _postRepository.GetByUserId(userId);
        return Result.Ok(posts.Select(post => _mapper.Map<GetPostDto>(post)).ToList());
    }

}