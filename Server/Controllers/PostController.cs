using Database.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;

namespace Server.Controllers;

[Route("api/posts")]
[ApiController]
public class PostController : Controller
{
    private readonly IPostService _postService;
    
    public PostController(IPostService postService) => _postService = postService;

    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
    {
        if(!await _postService.CreatePostAsync(dto)) return BadRequest("User ID is invalid");
        return Ok("Post created");
    }
    
    [HttpGet("get/by-id/{id}")]
    public async Task<IActionResult> GetPost([FromQuery] Guid id)
    {
        PostDto? post = await _postService.GetPostAsync(id);
        if(post is null) return NotFound();
        return Ok(post);
    }

    [HttpGet("get/by-title/{title}")]
    public async Task<IActionResult> GetPost([FromQuery] string title)
    {
        PostDto? post = await _postService.GetPostAsync(title);
        if(post is null) return NotFound();
        return Ok(post);
    }

    [HttpGet("get/all")]
    public async Task<List<PostDto>?> GetAllPosts()
    {
        return await _postService.GetPostsAsync();
    }

    [HttpGet("get/all/{userNickname}")]
    public async Task<List<PostDto>?> GetAllPosts(string userNickname)
    {
        return await _postService.GetPostsByUserNicknameAsync(userNickname);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDto dto)
    {
        if(!await _postService.UpdatePostAsync(dto)) return BadRequest("Post IF is invalid");
        return Ok("Post updated");
    }

    [HttpDelete("delete/by-id/{id}")]
    public async Task<IActionResult> DeletePost(Guid postId)
    {
        if(!await _postService.DeletePostAsync(postId)) return BadRequest("This Post is already deleted or invalid ID");
        return Ok("Post deleted");
    }

    [HttpDelete("delete/by-title/{title}")]
    public async Task<IActionResult> DeleteAllPosts(string title)
    {
        if(!await _postService.DeletePostAsync(title)) return BadRequest("This Post is already deleted or the title is invalid");
        return Ok("Post deleted");
    }
}