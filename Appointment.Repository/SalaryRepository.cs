using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;
namespace VUPayRoll.Repository
{
   public class SalaryRepository : IVUPayRoll<Salary>, ISalaryRepository
    {
        private readonly VUPayRollDBContext _context = null;
        public SalaryRepository(VUPayRollDBContext context)
        {
            this._context = context;
        }
        public async Task<Salary> Get(int Id)
        {
            return await _context.Salaries.Include(x=>x.Employee).Where(x=>x.EmployeeId==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Salary>> GetAll()
        {
            return await _context.Salaries.Include(x => x.Employee).ToListAsync();
        }
        public IEnumerable<Salary> GetAllS()
        {
            return  _context.Salaries.Include(x => x.Employee);
        }

        public async Task<Salary> Add(Salary model)
        {
            await _context.Set<Salary>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Salary> Update(Salary model)
        {
            var customer = _context.Set<Salary>().Attach(model);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Salary> Delete(int id)
        {

            Salary entity = await _context.Set<Salary>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Salary>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }
    }
}
