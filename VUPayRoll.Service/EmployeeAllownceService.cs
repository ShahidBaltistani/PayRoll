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
    public class EmployeeAllowanceService
    {
        private readonly IVUPayRoll<EmployeeAllowance> _repo;
        private readonly IEmployeeAllowanceRepository _repo2;
        private readonly IMapper _mapper;

        public EmployeeAllowanceService(IVUPayRoll<EmployeeAllowance> repo, IEmployeeAllowanceRepository repo2, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }

        public async Task<IEnumerable<EmployeeAllowanceViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<EmployeeAllowanceViewModel>>(data);
            return employee;
        }

        public async Task<EmployeeAllowanceViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<EmployeeAllowanceViewModel>(employee);
            return employeeVM;
        }

        public async Task<EmployeeAllowance> Add(EmployeeAllowanceViewModel employeeAllowanceViewModel)
        {
            var userM = _mapper.Map<EmployeeAllowance>(employeeAllowanceViewModel);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<EmployeeAllowance> Update(EmployeeAllowanceViewModel employeeAllowanceViewModel)
        {
            var userM = _mapper.Map<EmployeeAllowance>(employeeAllowanceViewModel);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<EmployeeAllowance> Delete(int id)
        {
            var employeeInfo = await _repo.Delete(id);
            return employeeInfo;
        }
        //Custom Code
        public async Task<IEnumerable<EmployeeAllowanceViewModel>> GetEmployeeAllowanceByEmployeeId(int Id)
        {
            var data = await _repo2.GetEmployeeAllowanceByEmployeeId(Id);
            var employee = _mapper.Map<IEnumerable<EmployeeAllowanceViewModel>>(data);
            return employee;
        }

        public async Task<IEnumerable<AllowanceTypeViewModel>> GetAllowancTypes()
        {
            var data = await _repo2.GetAllowancTypes();
            var result = _mapper.Map<IEnumerable<AllowanceTypeViewModel>>(data);
            return result;
        }
    }
}
