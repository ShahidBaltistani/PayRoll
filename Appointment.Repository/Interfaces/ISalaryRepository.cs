using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
   public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetAll();
        IEnumerable<Salary> GetAllS();
    }
}
