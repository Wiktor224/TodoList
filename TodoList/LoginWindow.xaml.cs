using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;
using TodoList.Data;
using TodoList.Interfaces.Services;
using TodoList.Models;

namespace TodoList
{
    public partial class LoginWindow : Window
    {
        public User LoggedInUser { get; private set; }

        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        public LoginWindow(AppDbContext context, IUserService userService)
        {
            InitializeComponent();
            _context = context;
            _userService = userService;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailInput.Text;
            string password = PasswordInput.Password;

            if (_userService.ValidateCredentials(email, password, out var user))
            {
                LoggedInUser = user;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy email lub hasło.");
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailInput.Text.Trim();
            string password = PasswordInput.Password;

            var user = _userService.CreateAccount(email, password, out var errorMessage);

            if (user != null)
            {
                MessageBox.Show("Utworzono konto.", $"Login : {email}", MessageBoxButton.OK, MessageBoxImage.Information);
                LoggedInUser = user;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
