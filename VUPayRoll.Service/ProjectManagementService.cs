using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;
using VUPayRoll.ViewModel;

namespace VUPayRoll.Service
{
    public class ProjectManagementService
    {
        private readonly IVUPayRoll<ProjectName> _repo;
        private readonly IProjectManagementRepository _repo2;
        private readonly IMapper _mapper;

        public ProjectManagementService(IVUPayRoll<ProjectName> repo, IProjectManagementRepository repo2, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }
        public async Task<IEnumerable<ProjectNameViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<ProjectNameViewModel>>(data);
            return employee;
        }
        public async Task<ProjectNameViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<ProjectNameViewModel>(employee);
            return employeeVM;
        }
        public async Task<ProjectName> Add(ProjectNameViewModel model)
        {
            var userM = _mapper.Map<ProjectName>(model);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }
        public async Task<ProjectName> Delete(int id)
        {
            var data = await _repo.Delete(id);
            return data;
        }

        //Custom
        public async Task<Projects> CreateProjects(ProjectsViewModel model)
        {
            var userM = _mapper.Map<Projects>(model);
            var res = await _repo2.CreateProjects(userM);
            return res;
        }
        public async Task<IEnumerable<ProjectsViewModel>> GetAllProjects()
        {
            var data = await _repo2.GetAllProjects();
            var employee = _mapper.Map<IEnumerable<ProjectsViewModel>>(data);
            return employee;
        }
        public async Task<IEnumerable<ProjectsViewModel>> DeleteProject(int PId)
        {
            var data = await _repo2.DeleteProject(PId);
            var employee = _mapper.Map<IEnumerable<ProjectsViewModel>>(data);
            return employee;
        }
        public async Task<TaskT> EmployeeTask(TaskViewModel model)
        {
            var userM = _mapper.Map<TaskT>(model);
            var employeeInfo = await _repo2.EmployeeTask(userM);
            return employeeInfo;
        }
        public async Task<IEnumerable<TaskViewModel>> GetAllTasks()
        {
            var data = await _repo2.Tasks();
            var employee = _mapper.Map<IEnumerable<TaskViewModel>>(data);
            return employee;
        }

        public async Task<TaskViewModel> DeleteTask(int Id,int EmpId, int PId)
        {
            var data = await _repo2.DeleteTask(Id,EmpId,PId);
            var employee = _mapper.Map<TaskViewModel>(data);
            return employee;
        }
    }
}
