using TodoList.Enums;
using TodoList.Interfaces.Repositories;
using TodoList.Interfaces.Services;
using TodoList.Models;

namespace TodoList.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUser(string email, string password)
        {
            if (_userRepository.GetByEmail(email) != null)
                return false; // użytkownik już istnieje

            var newUser = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = Enums.UserRole.User // domyślnie
            };

            _userRepository.AddUser(newUser);
            return true;
        }

        public bool EditUser(User user)
        {
            if (_userRepository.GetByEmail(user.Email) == null)
                return false;

            _userRepository.UpdateUser(user);
            return true;
        }

        public bool DeleteUser(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return false;

            _userRepository.DeleteUser(user);
            return true;
        }

        public bool ValidateCredentials(string email, string password, out User user)
        {
            user = _userRepository.GetByEmail(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return true;
            }
            return false;
        }

        public User CreateAccount(string email, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            var existingUser = _userRepository.GetByEmail(email);
            if (existingUser != null)
            {
                errorMessage = "Użytkownik o tym adresie e-mail już istnieje.";
                return null;
            }

            var newUser = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = UserRole.User
            };

            _userRepository.AddUser(newUser);
            return GetUser(newUser.Email);
        }

        public User GetUser(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}
