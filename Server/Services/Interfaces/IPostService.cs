using Database.DTO;
using Database.Models;

namespace Server.Services.Interfaces;

public interface IPostService
{
    Task<bool> CreatePostAsync(CreatePostDto post);
    Task<bool> UpdatePostAsync(UpdatePostDto post);
    Task<bool> DeletePostAsync(Guid id);
    Task<bool> DeletePostAsync(string title);
    Task<PostDto?> GetPostAsync(Guid id);
    Task<PostDto?> GetPostAsync(string title);
    Task<List<PostDto>?> GetPostsAsync();
    Task<List<PostDto>?> GetPostsByUserNicknameAsync(string nickname);
}