using TodoList.Models;

namespace TodoList.Interfaces.Services
{
    public interface IUserService
    {
        bool AddUser(string email, string password);
        bool EditUser(User user);
        bool DeleteUser(string email);
        bool ValidateCredentials(string email, string password, out User user);
        User CreateAccount(string email, string password, out string errorMessage);

    }
}
