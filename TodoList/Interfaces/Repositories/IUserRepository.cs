using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User userToAdd);
        User GetByEmail(string email);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
