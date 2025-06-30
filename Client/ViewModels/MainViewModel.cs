using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Client.Models;
using Client.Models.DTO.CommentDtos;
using Client.Models.DTO.PostDtos;
using Client.Services;

namespace Client.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly PostService _postService;
    private readonly UserService _userService;
    
    public ObservableCollection<PostDto>  Posts { get; set; } = new ObservableCollection<PostDto> { new PostDto()
    {
        Id = Guid.NewGuid(), 
        Title = "Hello programming world",
        Content = "It's the large world and I want to be a part of it. Next will be something cooler than that",
        UserNickname = "Andrew",
        Created = DateTime.Now,
        Comments = new List<Comment>(),
        Likes = 0, Dislikes = 0
    },
        new PostDto()
        {
            Id = Guid.NewGuid(),
            Title = "Ukraine",
            Content = "Every day, Ukraine resists Russia's full-scale invasion. From the moment Russian troops crossed the Ukrainian border, \nbringing only pain, death and destruction, Ukrainian lives have not been the same. But we are standing firm against aggression. \nOur resilience is rooted in the courage of our people, who defend our home and our values with all their hearts.\n\n",
            UserNickname = "Andrew",
            Created = DateTime.Now,
            Comments = new List<Comment>(),
            Likes = 0, Dislikes = 0
        }
    };
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        _postService = new PostService();
        _ = LoadPostsAsync();
    }

    private async Task LoadPostsAsync()
    {
        try
        {
            var postsFromApi = await _postService.GetPosts();
            Posts.Clear();

            foreach (var post in postsFromApi)
            {
                Posts.Add(post);
                Console.WriteLine(post.Id + " " + post.Title);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error in getting posts: {e.Message}");
        }
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}