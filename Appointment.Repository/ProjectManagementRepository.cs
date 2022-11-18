using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;

namespace VUPayRoll.Repository
{
    public class ProjectManagementRepository : IVUPayRoll<ProjectName>, IProjectManagementRepository
    {
        private readonly VUPayRollDBContext _context;
        public ProjectManagementRepository(VUPayRollDBContext context)
        {
            _context = context;
        }
        public async Task<ProjectName> Add(ProjectName t)
        {
            string SDate= Convert.ToDateTime(t.StartDate).ToShortDateString();
            t.StartDate = SDate;
            await _context.Set<ProjectName>().AddAsync(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<ProjectName> Delete(int id)
        {
            var entity = await _context.Set<ProjectName>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<ProjectName>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<ProjectName> Get(int Id)
        {
            return await _context.Set<Entities.ProjectName>().FindAsync(Id);
        }

        public async Task<IEnumerable<ProjectName>> GetAll()
        {
            return await _context.Set<ProjectName>().ToListAsync();
        }

        public Task<ProjectName> Update(ProjectName tUpdation)
        {
            throw new NotImplementedException();
        }

        public async Task<Projects> CreateProjects(Projects project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<IEnumerable<Projects>> GetAllProjects()
        {
            return await _context.Projects.Include(x => x.ProjectName).Include(x => x.Employee).ToListAsync();
        }

        public async Task<IEnumerable<Projects>> DeleteProject(int PId)
        {
         
            List<Projects> Projects =await _context.Projects.Where(x => x.ProjectNameId == PId).ToListAsync();
            foreach (var item in Projects)
            {
                item.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return Projects;
        }

        // Task Management
        public async Task<TaskT> EmployeeTask(TaskT task)
        {
            task.IsDelete = false;
            task.CreatedDate = DateTime.Now.Date;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<TaskT>> Tasks()
        {
            return await _context.Tasks.Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<TaskT> DeleteTask(int Id,int EmpId, int PId)
        {
            var task =await _context.Tasks.Where(x => x.EmpId == EmpId && x.ProjId == PId && x.ID==Id).FirstOrDefaultAsync();
            task.IsDelete = true;
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
