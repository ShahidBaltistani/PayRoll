using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.Entities;
using System.Threading.Tasks;

namespace VUPayRoll.Repository.Interfaces
{
   public interface IDocumentRepository
   {
        Task<IEnumerable<Document>> GetDocumentByEmployeeId(int id);
    }
}
