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
    public class LeaveRepository : IVUPayRoll<Leave>,ILeaveRepository
    {
        private readonly VUPayRollDBContext _context;
        public LeaveRepository(VUPayRollDBContext context)
        {
            _context = context;
        }
        public async Task<Leave> Add(Leave t)
        {
            await _context.Set<Leave>().AddAsync(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<Leave> Delete(int id)
        {
            Leave entity = await _context.Set<Leave>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Leave>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<Leave> Get(int Id)
        {
            return await _context.Set<Leave>().FindAsync(Id);
        }

        public async Task<IEnumerable<Leave>> GetAll()
        {
            return await _context.Leaves.Include(x=>x.User).OrderByDescending(x=>x.ID).ToListAsync();
        }

        public async Task<Leave> Update(Leave tUpdation)
        {
            var customer = _context.Set<Leave>().Attach(tUpdation);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return tUpdation;
        }


        //Custom
        public async Task<Leave> Approved(int id)
        {
            var leave = (await _context.Leaves.FirstOrDefaultAsync(x => x.ID == id));
            leave.IsApproved = ApprovalStatus.Approved;
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<IEnumerable<Leave>> GetAllNotApprovedLeaveForAdminDashboard()
        {
            return await _context.Leaves.Include(x => x.User).Where(x=>x.IsApproved==ApprovalStatus.InProcess).OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<IEnumerable<Leave>> GetAllByUserId(int UserId)
        {
            return await _context.Leaves.Include(x => x.User).Where(x => x.UserId == UserId).OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<Leave> Rejected(int id)
        {
            var leave = (await _context.Leaves.FirstOrDefaultAsync(x => x.ID == id));
            leave.IsApproved = ApprovalStatus.Rejected;
            await _context.SaveChangesAsync();
            return leave;
        }
    }
}
