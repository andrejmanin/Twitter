using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.ObjectModel;
using Client.Models.DTO.PostDtos;

namespace Client.ViewModels;

public class PostService
{
    private readonly HttpClient _httpClient;

    public PostService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5036/api/posts");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<ObservableCollection<PostDto>> GetPosts()
    {
        var posts = await _httpClient.GetFromJsonAsync<ObservableCollection<PostDto>>("api/posts/get/all");
        return posts ??  new ObservableCollection<PostDto>();
    }
}