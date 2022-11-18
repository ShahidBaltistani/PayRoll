using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
   public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> SignUp(User user);
        Task<User> Login(string username, string password);
        Task<bool> UserExist(string username);
        Task<User> Approved(int id);

        //Custom
        Task<IEnumerable<User>> GetAlls();
    }
}
