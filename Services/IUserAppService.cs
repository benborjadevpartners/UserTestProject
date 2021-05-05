using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestProject.Models;

namespace UserTestProject.Services
{
    public interface IUserAppService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> AddUser(User user);
        Task<User> EditUser(int id,User user);
        Task<int> DeleteUser(int id);
        Task<User> ValidateLogin(string username, string password);
    }
}
