using System.Net.Http.Json;
using System.Collections.ObjectModel;
using Client.Models.DTO.PostDtos;
using Client.ViewModels;

namespace Client.Services;

public class PostService
{
    public async Task<ObservableCollection<PostDto>> GetPosts()
    {
        string url = "api/posts/get/all";
        var posts = await HttpClientBase.HttpClient.GetFromJsonAsync<ObservableCollection<PostDto>>(url);
        return posts ??  new ObservableCollection<PostDto>();
    }

    public async Task CreatePost(CreatePostDto post)
    {
        string url = "api/posts/createUser";

        var response = await HttpClientBase.HttpClient.PostAsJsonAsync(url, post);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error creating post: {response.StatusCode}, {errorContent}");
        }
    }
}