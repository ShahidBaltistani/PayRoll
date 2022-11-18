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
   public class AnnouncementRepository : IVUPayRoll<Announcement>
    {
        private readonly VUPayRollDBContext _context;
        public AnnouncementRepository(VUPayRollDBContext context)
        {
            _context = context;
        }
        public async Task<Announcement> Get(int Id)
        {
            return await _context.Set<Announcement>().FindAsync(Id);
        }

        public async Task<IEnumerable<Announcement>> GetAll()
        {
            return await _context.Set<Announcement>().OrderByDescending(x=>x.ID).ToListAsync();
        }

        public async Task<Announcement> Add(Announcement model)
        {
            model.IsDelete = false;
            await _context.Set<Announcement>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Announcement> Update(Announcement model)
        {
            var customer = _context.Set<Announcement>().Attach(model);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Announcement> Delete(int id)
        {

            var entity = await _context.Set<Announcement>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return entity;
        }


    }
}
