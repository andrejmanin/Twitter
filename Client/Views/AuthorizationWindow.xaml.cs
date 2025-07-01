using System.Windows;
using Client.ViewModels;

namespace Client.Views;

public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
        InitializeComponent();
    }

    private async void SignInButtonClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string email = EmailField.Text;
            string password = PasswordField.Password;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email or password is empty");
                return;
            }
            AuthorizationViewModel vm = new AuthorizationViewModel();
            var result = await vm.CheckUser(email, password);
            if (!result)
            {
                MessageBox.Show("Email or password is incorrect");
                return;
            }
            var mainWindow = new MainWindow(email);
            mainWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void SingUpButtonClick(object sender, RoutedEventArgs e)
    {
        try
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}