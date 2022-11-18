using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.Entities;
using System.Threading.Tasks;

namespace VUPayRoll.Repository.Interfaces
{
    public interface IDependentRepository
    {
        Task<IEnumerable<Dependent>> GetDependentByEmployeeId(int id);
        Task<IEnumerable<RelationshipType>> RelationshipTypes();
    }
}
