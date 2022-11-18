using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
    public interface ILeaveRepository
    {
        Task<Leave> Approved(int id);
        Task<Leave> Rejected(int id);
        Task<IEnumerable<Leave>> GetAllNotApprovedLeaveForAdminDashboard();
        Task<IEnumerable<Leave>> GetAllByUserId(int UserId);
    }
}
