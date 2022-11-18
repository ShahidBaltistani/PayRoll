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
   public class EmployeeAllowanceRepository : IVUPayRoll<EmployeeAllowance>,IEmployeeAllowanceRepository
    {

        private readonly VUPayRollDBContext _context = null;
        public EmployeeAllowanceRepository(VUPayRollDBContext context)
        {
            this._context = context;
        }
        public async Task<EmployeeAllowance> Get(int Id)
        {
            return await _context.Set<EmployeeAllowance>().FindAsync(Id);
        }

        public async Task<IEnumerable<EmployeeAllowance>> GetAll()
        {
            return await _context.Set<EmployeeAllowance>().Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<EmployeeAllowance> Add(EmployeeAllowance model)
        {
            model.IsDelete = false;
            await _context.Set<EmployeeAllowance>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<EmployeeAllowance> Update(EmployeeAllowance model)
        {
            var customer = _context.Set<EmployeeAllowance>().Attach(model);
            customer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<EmployeeAllowance> Delete(int id)
        {

            EmployeeAllowance entity = await _context.Set<EmployeeAllowance>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<EmployeeAllowance>> GetEmployeeAllowanceByEmployeeId(int id)
        {
            return await _context.EmployeeAllowances.Include(x => x.Allowance).Include(x => x.Employee).Where(x => x.EmployeeId == id && x.IsDelete==false).ToListAsync();
        }

        public async Task<IEnumerable<AllowanceType>> GetAllowancTypes()
        {
            return await _context.AllowanceTypes.ToListAsync();
        }
    }
}
