using System.Collections.ObjectModel;
using System.Net.Http.Json;
using Client.Models.DTO.UserDtos;

namespace Client.Services;

public class UserService
{
    public async Task<UserDto> GetUserByEmail(string email)
    {
        string url = $"api/user/getUser/{Uri.EscapeDataString(email)}";
        var user = await HttpClientBase.HttpClient.GetFromJsonAsync<UserDto>(url);
        return user ??  new UserDto();
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        string url = $"api/user/getUser/{id}";
        var user = await HttpClientBase.HttpClient.GetFromJsonAsync<UserDto>(url);
        return user ??  new UserDto();
    }
    
    public async Task<bool> CheckUser(string email, string password)
    {
        string url = $"api/user/checkUser/?email={email}&password={password}";
        var isValid = await HttpClientBase.HttpClient.GetFromJsonAsync<bool>(url);
        return isValid;
    }

    public static async Task CreateNewUser(CreateUserDto dto)
    {
        string url = $"api/user/createUser";
        var responce = await HttpClientBase.HttpClient.PostAsJsonAsync(url, dto);
        if (!responce.IsSuccessStatusCode)
        {
            var error = await responce.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}