using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.Entities;
using System.Threading.Tasks;

namespace VUPayRoll.Repository.Interfaces
{
    public interface IEmployeeAllowanceRepository
    {
        Task<IEnumerable<EmployeeAllowance>> GetEmployeeAllowanceByEmployeeId(int id);
        Task<IEnumerable<AllowanceType>> GetAllowancTypes();
    }
}
