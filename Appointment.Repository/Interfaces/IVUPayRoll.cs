using VUPayRoll.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace VUPayRoll.Repository.Interfaces
{
    public interface IVUPayRoll<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int Id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add(TEntity t);
        Task<TEntity> Update(TEntity tUpdation);
        Task<TEntity> Delete(int id);
        
    }
}
