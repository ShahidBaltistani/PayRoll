using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;
using System.Linq;


namespace VUPayRoll.Repository
{
   public class AccountRepository : IAccountRepository
    {
        private readonly VUPayRollDBContext _context;
        public AccountRepository(VUPayRollDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(x => x.Role).Where(x=>x.IsApproved==false).ToListAsync();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            return user;
        }

        public async Task<User> SignUp(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }  
        public async Task<User> Approved(int id)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.ID == id));
            user.IsApproved = true;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAlls()
        {
            return await _context.Users.Include(x => x.Role).Where(x=>x.RoleId!=1).ToListAsync();
        }
    }
}
