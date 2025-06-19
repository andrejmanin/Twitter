using Database;
using Database.DTO.CommentDtos;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Server.Services.Interfaces;

namespace Server.Services;

public class CommentService : ICommentService
{
    private readonly TwitterDbContext _context;
    
    public CommentService(TwitterDbContext context) => _context = context;
    
    public async Task<bool> CreateCommentAsync(CreateCommentDto commentDto)
    {
        var userId = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Nickname == commentDto.UserNickname);
        if(userId is null) return false;
        
        await _context.Comments.AddAsync(new Comment
        {
            PostId = commentDto.PostId,
            UserId = userId.Id,
            Text = commentDto.Text,
            Created = DateTime.Now
        });
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId)
    {
        return await _context.Comments.AsNoTracking().Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<Comment?> GetCommentAsync(Guid commentId)
    {
        return await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<bool> UpdateCommentAsync(UpdateCommentDto comment)
    {
        var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
        if(commentToUpdate is null) return false;
        
        commentToUpdate.Text = comment.Text;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCommentAsync(Guid commentId)
    {
        var commentToDelete = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == commentId);
        if(commentToDelete is null) return false;
        _context.Comments.Remove(commentToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}