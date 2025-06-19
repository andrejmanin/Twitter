using Database.DTO;
using Database.Models;

namespace Server.Services.Interfaces;

public interface IPostService
{
    Task<bool> CreatePostAsync(CreatePostDto post);
    Task<bool> UpdatePostAsync(UpdatePostDto post);
    Task<bool> DeletePostAsync(Guid id);
    Task<bool> DeletePostAsync(string title);
    Task<Post?> GetPostAsync(Guid id);
    Task<Post?> GetPostAsync(string title);
    Task<List<Post>?> GetPostsAsync();
    Task<List<Post>?> GetPostsByUserNicknameAsync(string nickname);
}