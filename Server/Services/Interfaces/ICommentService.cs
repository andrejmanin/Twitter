using Database.DTO.CommentDtos;
using Database.Models;

namespace Server.Services.Interfaces;

public interface ICommentService
{
    public Task<bool> CreateCommentAsync(CreateCommentDto commentDto);
    public Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId);
    public Task<Comment?> GetCommentAsync(Guid commentId);
    public Task<bool> UpdateCommentAsync(UpdateCommentDto comment);
    public Task<bool> DeleteCommentAsync(Guid commentId);
}