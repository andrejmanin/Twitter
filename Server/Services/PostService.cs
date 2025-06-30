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

    public async Task<PostDto?> GetPostAsync(Guid id)
    {
        Post? post = await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if(post is null) return  null;
        return await CreateDtoPost(post);
    }

    public async Task<PostDto?> GetPostAsync(string title)
    {
        Post? post = await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Title == title);
        if (post is null) return null;
        return await CreateDtoPost(post);
    }

    public async Task<List<PostDto>?> GetPostsAsync()
    {
        List<Post> posts = await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .ToListAsync();
        
        List<PostDto> postDtos = new List<PostDto>();
        foreach (var post in posts)
        {
            PostDto? postDto = await CreateDtoPost(post);
            if(postDto is null) continue;
            postDtos.Add(postDto);
        }
        return postDtos;
    }

    public async Task<List<PostDto>?> GetPostsByUserNicknameAsync(string nickname)
    {
        List<Post> posts = await _context.Posts
            // .Include(p => p.User)    ---- Under question.
            .Include(p => p.Comments)
            .AsNoTracking()
            .Where(p => p.User.Nickname == nickname)
            .ToListAsync();
        
        List<PostDto> postDtos = new List<PostDto>();
        foreach (var post in posts)
        {
            PostDto? postDto = await CreateDtoPost(post);
            if(postDto is null) continue;
            postDtos.Add(postDto);
        }
        return postDtos;
    }
    
    private async Task<bool> DeletePostAsync(Post post)
    {
        if(post.Comments.Any()) _context.Comments.RemoveRange(post.Comments);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<PostDto?> CreateDtoPost(Post post)
    {
        var userNickname = await _context.Users.FirstOrDefaultAsync(u => u.Id == post.UserId);
        if(userNickname is null) return null;
        return new PostDto
        {
            Id = post.Id,
            UserNickname = userNickname.Nickname,
            Title = post.Title,
            Content = post.Content,
            Created = post.Created,
            Likes = post.Likes,
            Dislikes = post.Dislikes,
            Comments = post.Comments,
        };
    }
}