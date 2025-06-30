using Database.DTO;
using Database.Models;

namespace Server.Services.Interfaces;

public interface IUserService
{
    Task<bool> CheckUser(string email, string password);
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto?> GetUserAsync(Guid id);
    Task<UserDto?> GetUserAsync(string email);
    Task<UserDto?> GetUserByNicknameAsync(string nickname);
    Task<string> CreateUserAsync(CreateUserDto userDto);
    Task<string> UpdateUserAsync(UpdateUserDto userDto); 
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> DeleteUserAsync(string email);
}