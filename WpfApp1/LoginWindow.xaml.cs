using System.Data;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private AuthService authService;
        public LoginWindow()
        {
            InitializeComponent();
            authService = new AuthService();

        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            Role role = authService.TryAuth(LoginTextBox.Text, PasswordTextBox.Password);
            if (role != null)
            {
                var mainWindow = new ProductForm(role);
                mainWindow.Show();
                Application.Current.Windows.OfType<LoginWindow>().First()?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click_Guest(object sender, RoutedEventArgs e)
        {
            Role role = new Role();
            var mainWindow = new ProductForm(role);
            mainWindow.Show();
            Application.Current.Windows.OfType<LoginWindow>().First()?.Close();
        }
    }
}