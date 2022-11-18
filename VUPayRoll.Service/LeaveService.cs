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
    public class LeaveService
    {
        private readonly IVUPayRoll<Leave> _repo;
        private readonly ILeaveRepository _repo2;
        private readonly IMapper _mapper;

        public LeaveService(IVUPayRoll<Leave> repo, ILeaveRepository repo2,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }
        public async Task<IEnumerable<LeaveViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<LeaveViewModel>>(data);
            return employee;
        }

        public async Task<LeaveViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<LeaveViewModel>(employee);
            return employeeVM;
        }

        public async Task<Leave> Add(LeaveViewModel leaveViewModel)
        {
            var userM = _mapper.Map<Leave>(leaveViewModel);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Leave> Update(LeaveViewModel leaveViewModel)
        {
            var userM = _mapper.Map<Leave>(leaveViewModel);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Leave> Delete(int id)
        {
            var employeeInfo = await _repo.Delete(id);
            return employeeInfo;
        }

        //Custom
        public async Task<LeaveViewModel> Rejected(int id)
        {
            var data = await _repo2.Rejected(id);
            var result = _mapper.Map<LeaveViewModel>(data);
            return result;
        }
        public async Task<LeaveViewModel> Approved(int id)
        {
            var data = await _repo2.Approved(id);
            var result = _mapper.Map<LeaveViewModel>(data);
            return result;
        }
        public async Task<IEnumerable<LeaveViewModel>> GetAllNotApprovedLeaveForAdminDashboard()
        {
            var data = await _repo2.GetAllNotApprovedLeaveForAdminDashboard();
            var employee = _mapper.Map<IEnumerable<LeaveViewModel>>(data);
            return employee;
        }
        public async Task<IEnumerable<LeaveViewModel>> GetAllByUserId(int UserId)
        {
            var data = await _repo2.GetAllByUserId(UserId);
            var employee = _mapper.Map<IEnumerable<LeaveViewModel>>(data);
            return employee;
        }
    }
}
