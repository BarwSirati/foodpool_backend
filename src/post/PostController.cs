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
        if (post.IsFailed)
        {
            switch (post.Reasons[0].Message)
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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<GetPostDto>>> GetAllPost(){
        var posts = await _postService.GetAll();
        if(posts.Value is null) return NotFound();
      
        return Ok(posts.Value);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<GetPostDto>> GetPostById(int id){
        if (_contextProvider.GetCurrentUser() != id) return Unauthorized();
        var post = await _postService.GetById(id); 
        if (post.Value is null) return NotFound();
        return Ok(post.Value);
    }
    

    [HttpGet("user/{id:int}")]
    [Authorize]
    public async Task<ActionResult<List<GetPostDto>>> GetPostByUserId(int userId){
        var posts = await _postService.GetByUserId(userId);
        if(posts.Value is null) return NotFound();
    
        return Ok(posts.Value);
    }

    [HttpPut("update/{id:int}")]
    [Authorize]
    public async Task<ActionResult<GetPostDto>> Update(UpdatePostDto updatePostDto, int id)
    {
        var post = await _postService.Update(updatePostDto, id);
        if (post.IsFailed)
        {
            switch (post.Reasons[0].Message)
            {
                case "404":
                    return NotFound();
                case "400":
                    return BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                
                
            }
        }

        return Ok(post.Value);
    }
}