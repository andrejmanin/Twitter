using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Client.Models.DTO.PostDtos;

namespace Client.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly PostService _postService;
    
    public ObservableCollection<PostDto>  Posts { get; set; } = new ObservableCollection<PostDto>();
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        _postService = new PostService();
        _ = LoadPostsAsync();
    }

    private async Task LoadPostsAsync()
    {
        var postsFromApi = await _postService.GetPosts();
        Posts.Clear();

        foreach (var post in postsFromApi)
        {
            Posts.Add(post);
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