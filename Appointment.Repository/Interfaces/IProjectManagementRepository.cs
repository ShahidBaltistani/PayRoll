using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
    public interface IProjectManagementRepository
    {
        Task<Projects> CreateProjects(Projects project);
        Task<IEnumerable<Projects>> GetAllProjects();
        Task<IEnumerable<Projects>> DeleteProject(int PId);

        //Task Management
        Task<TaskT> EmployeeTask(TaskT task);
        Task<IEnumerable<TaskT>> Tasks();
        Task<TaskT> DeleteTask(int Id,int EmpId,int PId);
    }
}
