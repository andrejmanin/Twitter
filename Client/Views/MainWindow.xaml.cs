using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ViewModels;

namespace Client.Views;

public partial class MainWindow : Window
{
    private string _email;
    
    public MainWindow(string email) 
    {
        InitializeComponent();
        _email = email;
        UpdateDataContext();
    }

    private void UpdatePostList(object sender, RoutedEventArgs e)
    {
        UpdateDataContext();
    }
    
    private void CreateNewPost(object sender, RoutedEventArgs e)
    {
        var createPostWindow = new CreatePostWindow(UserNickname.Text);
        createPostWindow.ShowDialog();
        UpdateDataContext();
    }

    private void UpdateDataContext()
    {
        DataContext = new MainViewModel(_email);
    }
}