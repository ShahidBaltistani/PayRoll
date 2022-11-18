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
   public class DependentRepository : IVUPayRoll<Dependent>,IDependentRepository
    {
        private readonly VUPayRollDBContext _context;
        public DependentRepository(VUPayRollDBContext context)
        {
            _context = context;
        }
        public async Task<Dependent> Get(int Id)
        {
            return await _context.Set<Dependent>().FindAsync(Id);
        }

        public async Task<IEnumerable<Dependent>> GetAll()
        {
            return await _context.Set<Dependent>().Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<Dependent> Add(Dependent model)
        {
            model.IsDelete = false;
            await _context.Set<Dependent>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Dependent> Update(Dependent model)
        {
            var customer = _context.Set<Dependent>().Attach(model);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Dependent> Delete(int id)
        {

            Dependent entity = await _context.Set<Dependent>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<Dependent>> GetDependentByEmployeeId(int id)
        {
            return await _context.Dependents.Include(x => x.Employee).Include(x => x.RelationshipType).Where(x => x.EmployeeId == id).ToListAsync();
        }

        public async Task<IEnumerable<RelationshipType>> RelationshipTypes()
        {
            return await _context.RelationshipTypes.ToListAsync();
        }
    }
}
