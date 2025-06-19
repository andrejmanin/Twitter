using Database.DTO.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;

namespace Server.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    
    public CommentController(ICommentService commentService) => _commentService = commentService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentDto comment)
    {
        if(!await _commentService.CreateCommentAsync(comment)) return BadRequest("User Id is invalid");
        return Ok("Comment created");
    }

    [HttpGet("get-list/{postId}")]
    public async Task<IActionResult> GetCommentsAsync(Guid postId)
    {
        return Ok(await _commentService.GetCommentsAsync(postId));
    }
    
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCommentAsync(Guid id)
    {
        return Ok(await _commentService.GetCommentAsync(id));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCommentAsync([FromBody] UpdateCommentDto comment)
    {
        if(!await _commentService.UpdateCommentAsync(comment)) return BadRequest("Comment Id is invalid");
        return Ok("Comment updated");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCommentAsync(Guid id)
    {
        if(!await _commentService.DeleteCommentAsync(id)) return BadRequest("Comment Id is invalid");
        return Ok("Comment deleted");
    }
}