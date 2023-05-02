using FoodPool.post.dtos;
using FoodPool.post.interfaces;
using FoodPool.provider.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FoodPool.post;

[ApiController]
[Route("api/post/")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IHttpContextProvider _contextProvider;

    public PostController(IPostService postService, IHttpContextProvider contextProvider)
    {
        _postService = postService;
        _contextProvider = contextProvider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreatePostDto createPostDto)
    {

        if (_contextProvider.GetCurrentUser() != createPostDto.UserId) return Forbid();

        var post = await _postService.Create(createPostDto);
        if (!post.IsFailed) return Ok();
        return post.Reasons[0].Message switch
        {
            "404" => NotFound(),
            "400" => BadRequest(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<GetPostDto>>> GetAllPost()
    {
        var posts = await _postService.GetAll(_contextProvider.GetCurrentUser());
        if (posts.Value is null) return NotFound();

        return Ok(posts.Value);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<GetPostDto>> GetPostById(int id)
    {
        if (_contextProvider.GetCurrentUser() != id) return Forbid();
        var post = await _postService.GetById(id);
        if (post.Value is null) return NotFound();
        return Ok(post.Value);
    }


    [HttpGet("user/{id:int}")]
    [Authorize]
    public async Task<ActionResult<List<GetPostDto>>> GetPostByUserId(int id)
    {
        if (_contextProvider.GetCurrentUser() != id) return Forbid();
        var posts = await _postService.GetByUserId(id);
        if (posts.Value is null) return NotFound();
        return Ok(posts.Value);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<UpdatePostDto>> Update(UpdatePostDto updatePostDto, int id)
    {
        var post = await _postService.Update(updatePostDto, id,_contextProvider.GetCurrentUser());
        if (!post.IsFailed) return Ok(post.Value);
        return post.Reasons[0].Message switch
        {
            "404" => NotFound(),
            "400" => BadRequest(),
            "403" => Forbid(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}