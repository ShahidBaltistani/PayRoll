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
   public class AttendanceService
    {
        private readonly IVUPayRoll<Attendance> _repo;
        private readonly IAttendanceRepository _repo2;
        private readonly IMapper _mapper;

        public AttendanceService(IVUPayRoll<Attendance> repo, IAttendanceRepository repo2, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }
        public async Task<IEnumerable<AttendanceViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<AttendanceViewModel>>(data);
            return employee;
        }

        public async Task<AttendanceViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<AttendanceViewModel>(employee);
            return employeeVM;
        }

        public async Task<Attendance> Add(AttendanceViewModel model)
        {
            var userM = _mapper.Map<Attendance>(model);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Attendance> Update(AttendanceViewModel model)
        {
            var userM = _mapper.Map<Attendance>(model);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Attendance> Delete(int id)
        {
            var data = await _repo.Delete(id);
            return data;
        }

        public async Task<AttendanceViewModel> Approved(int id)
        {
            var data = await _repo2.Approved(id);
            var result = _mapper.Map<AttendanceViewModel>(data);
            return result;
        }

        public async Task<IEnumerable<AttendanceViewModel>> GetAllByUserId(int UserId)
        {
            var data = await _repo2.GetAllByUserId(UserId);
            var employee = _mapper.Map<IEnumerable<AttendanceViewModel>>(data);
            return employee;
        }
        public async Task<IEnumerable<AttendanceViewModel>> NotApproved()
        {
            var data = await _repo2.NotApproved();
            var result = _mapper.Map<IEnumerable<AttendanceViewModel>>(data);
            return result;

        }
        public async Task<AttendanceViewModel> Rejected(int id)
        {
            var data = await _repo2.Rejected(id);
            var result = _mapper.Map<AttendanceViewModel>(data);
            return result;
        }
        
    }
}
