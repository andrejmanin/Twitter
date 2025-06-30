using System.Net.Http.Json;
using System.Collections.ObjectModel;
using Client.Models.DTO.PostDtos;

namespace Client.Services;

public class PostService
{
    public async Task<ObservableCollection<PostDto>> GetPosts()
    {
        string url = "api/posts/get/all";
        var posts = await HttpClientBase.HttpClient.GetFromJsonAsync<ObservableCollection<PostDto>>(url);
        Console.WriteLine(posts);
        return posts ??  new ObservableCollection<PostDto>();
    }
}