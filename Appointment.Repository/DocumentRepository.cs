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
   public class DocumentRepository : IVUPayRoll<Document>,IDocumentRepository
    {
        private readonly VUPayRollDBContext _context = null;
        public DocumentRepository(VUPayRollDBContext context)
        {
            this._context = context;
        }
        public async Task<Entities.Document> Get(int Id)
        {
            return await _context.Set<Entities.Document>().FindAsync(Id);
        }

        public async Task<IEnumerable<Entities.Document>> GetAll()
        {
            return await _context.Documents.Include(x=>x.Employee).Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<Entities.Document> Add(Entities.Document model)
        {
            model.IsDelete = false;
            await _context.Set<Entities.Document>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Entities.Document> Update(Entities.Document model)
        {
            var customer = _context.Set<Entities.Document>().Attach(model);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Entities.Document> Delete(int id)
        {

            Entities.Document entity = await _context.Set<Entities.Document>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<Entities.Document>> GetDocumentByEmployeeId(int id)
        {
            return await _context.Documents.Include(x => x.Employee).Where(x => x.EmployeeId == id && x.IsDelete==false).ToListAsync();
        }
    }
}
