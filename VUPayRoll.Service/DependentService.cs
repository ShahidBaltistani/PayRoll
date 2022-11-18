using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;
using VUPayRoll.Repository;
using VUPayRoll.ViewModel;
using VUPayRoll.Repository.Interfaces;
namespace VUPayRoll.Service
{
    public class DependentService
    {
        private readonly IVUPayRoll<Dependent> _repo;
        private readonly IDependentRepository _repo2;
        private readonly IMapper _mapper;

        public DependentService(IVUPayRoll<Dependent> repo, IDependentRepository repo2, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }

        public async Task<IEnumerable<DependentViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<DependentViewModel>>(data);
            return employee;
        }

        public async Task<DependentViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<DependentViewModel>(employee);
            return employeeVM;
        }

        public async Task<Dependent> Add(DependentViewModel dependentViewModel)
        {
            var userM = _mapper.Map<Dependent>(dependentViewModel);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Dependent> Update(DependentViewModel dependentViewModel)
        {
            var userM = _mapper.Map<Dependent>(dependentViewModel);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Dependent> Delete(int id)
        {
            var employeeInfo = await _repo.Delete(id);
            return employeeInfo;
        }

        //Custom Code
        public async Task<IEnumerable<DependentViewModel>> GetDependentByEmployeeId(int Id)
        {
            var data = await _repo2.GetDependentByEmployeeId(Id);
            var employee = _mapper.Map<IEnumerable<DependentViewModel>>(data);
            return employee;
        }

        public async Task<IEnumerable<RelationshipTypeViewModel>> GetRelationshipTypes()
        {
            var data = await _repo2.RelationshipTypes();
            var result = _mapper.Map<IEnumerable<RelationshipTypeViewModel>>(data);
            return result;
        }
    }
}
