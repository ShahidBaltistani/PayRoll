using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
   public interface IAttendanceRepository
    {
        Task<Attendance> Approved(int id);
        Task<Attendance> Rejected(int id);
        Task<IEnumerable<Attendance>> NotApproved();
        Task<IEnumerable<Attendance>> GetAllByUserId(int UserId);
    }
}
