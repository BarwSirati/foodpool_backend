using FoodPool.post.dtos;
using FoodPool.post.interfaces;
using FoodPool.provider.interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> Create(CreatePostDto createPostDto)
    {
        var post = await _postService.Create(createPostDto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<GetPostDto>>> GetAllPost(){
        var post = await _postService.GetAll();
        return Ok(post.Value);
    }
}