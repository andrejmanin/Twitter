using System.Windows;
using Client.Models.DTO.UserDtos;
using Client.Services;

namespace Client.Views;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }
    
    private async void Register_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string nickname = NicknameBox.Text.Trim();
            string country = CountryBox.Text.Trim();
            string city = CityBox.Text.Trim();
        

            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не збігаються.");
                return;
            }

            var user = new CreateUserDto
            {
                Gmail = email,
                Password = password,
                Nickname = nickname,
                CountryName = country,
                CityName = city
            };

            try
            {
                await UserService.CreateNewUser(user);
                MessageBox.Show("Реєстрація успішна!");
                MainWindow window = new MainWindow(user.Gmail);
                window.Show();
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Creating error. {ex}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка: {ex.Message}");
        }
    }

    private void LogIn_Click(object sender, RoutedEventArgs e)
    {
        new AuthorizationWindow().Show();
        this.Close();
    }

}