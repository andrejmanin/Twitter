using Database;
using Database.DTO;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Server.Services.Interfaces;

namespace Server.Services;

public class PostService : IPostService
{
    private readonly TwitterDbContext _context;
    
    public PostService(TwitterDbContext context) => _context = context;
    
    public async Task<bool> CreatePostAsync(CreatePostDto post)
    {
        var userId = await _context.Users
            .FirstOrDefaultAsync(u => u.Nickname == post.UserNickname);
        if (userId is null) return false;
        
        Post newPost = new Post
        {
            UserId = userId.Id,
            Title = post.Title,
            Content = post.Content,
            Created = DateTime.Now,
            Likes = 0,
            Dislikes = 0,
            Comments = new List<Comment>()
        };
        await _context.Posts.AddAsync(newPost);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdatePostAsync(UpdatePostDto post)
    {
        var isPost = await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == post.Id);
        if (isPost is null) return false;
        
        isPost.Title = post.Title;
        isPost.Content = post.Content;
        isPost.Created = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePostAsync(Guid id)
    {
        Post? post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (post is null) return false;
        return await DeletePostAsync(post);
    }

    public async Task<bool> DeletePostAsync(string title)
    {
        Post? post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Title == title);
        
        if (post is null) return false;
        return await DeletePostAsync(post);
    }

    public async Task<Post?> GetPostAsync(Guid id)
    {
        return await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post?> GetPostAsync(string title)
    {
        return await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<List<Post>?> GetPostsAsync()
    {
        return await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Post>?> GetPostsByUserNicknameAsync(string nickname)
    {
        return await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .Where(p => p.User.Nickname == nickname)
            .ToListAsync();
    }
    
    private async Task<bool> DeletePostAsync(Post post)
    {
        if(post.Comments.Any()) _context.Comments.RemoveRange(post.Comments);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }
}