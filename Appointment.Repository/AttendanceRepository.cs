using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.Repository
{
   public class AttendanceRepository : IVUPayRoll<Attendance>, IAttendanceRepository
    {
        private readonly VUPayRollDBContext _context;
        public AttendanceRepository(VUPayRollDBContext context)
        {
            _context = context;
        }
        public async Task<Attendance> Get(int Id)
        {
            return await _context.Set<Attendance>().FindAsync(Id);
        }

        public async Task<IEnumerable<Attendance>> GetAll()
        {
            return await _context.Set<Attendance>().ToListAsync();
        }

        public async Task<Attendance> Add(Attendance model)
        {
            await _context.Set<Attendance>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Attendance> Update(Attendance model)
        {
            var customer = _context.Set<Attendance>().Attach(model);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Attendance> Delete(int id)
        {

            var entity = await _context.Set<Attendance>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Attendance>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<Attendance>> GetAllByUserId(int UserId)
        {
            return await _context.Attendances.Include(x => x.User).Where(x => x.UserId == UserId).OrderByDescending(x => x.ID).ToListAsync();
        }
        public async Task<Attendance> Approved(int id)
        {
            var leave = (await _context.Attendances.FirstOrDefaultAsync(x => x.ID == id));
            leave.IsApproved = ApprovalStatus.Approved;
            await _context.SaveChangesAsync();
            return leave;
        }

       public async Task<IEnumerable<Attendance>> NotApproved()
        {
            return await _context.Attendances.Include(x => x.User).Where(x => x.IsApproved == ApprovalStatus.InProcess).ToListAsync();

        }

        public async Task<Attendance> Rejected(int id)
        {
            var leave = (await _context.Attendances.FirstOrDefaultAsync(x => x.ID == id));
            leave.IsApproved = ApprovalStatus.Rejected;
            await _context.SaveChangesAsync();
            return leave;
        }

   
    }
}
