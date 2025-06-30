using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Client.Services;

namespace Client.ViewModels;

public class AuthorizationViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly UserService _userService;

    public AuthorizationViewModel()
    {
        _userService = new UserService();
    }

    public async Task<bool> CheckUser(string email, string password)
    {
        try
        {
            var result = await _userService.CheckUser(email, password);
            return result;
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error in checking user info: {e.Message}");
            return false;
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