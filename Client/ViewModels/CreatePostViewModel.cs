using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Client.Models.DTO.PostDtos;
using Client.Services;

namespace Client.ViewModels;

public class CreatePostViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly PostService _postService;

    public CreatePostViewModel()
    {
        _postService = new PostService();
    }

    public async void CreatePost(CreatePostDto post)
    {
        try
        {
            await _postService.CreatePost(post);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
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