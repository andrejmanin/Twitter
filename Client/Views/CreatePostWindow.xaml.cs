using System.Windows;
using System.Windows.Controls;
using Client.Models.DTO.PostDtos;
using Client.ViewModels;

namespace Client.Views;

public partial class CreatePostWindow : Window
{
    private string _username;
    public CreatePostWindow(string username)
    {
        InitializeComponent();
        _username = username;
    }
    
    private void PublishButton_Click(object sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text;
        string body = BodyBox.Text;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
        {
            MessageBox.Show("Будь ласка, заповніть всі поля.");
            return;
        }
        CreatePostViewModel  vm = new CreatePostViewModel();
        CreatePostDto post = new CreatePostDto
        {
            UserNickname = _username,
            Title = title,
            Content = body
        };
        vm.CreatePost(post);
        
        MessageBox.Show("Пост створено!");
    
        TitleBox.Text = "";
        BodyBox.Text = "";
        this.Close();
    }

}