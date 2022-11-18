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
   public class AnnouncementService
    {
        private readonly IVUPayRoll<Announcement> _repo;
        //private readonly IAnnouncementRepository _repo2;
        private readonly IMapper _mapper;

        public AnnouncementService(IVUPayRoll<Announcement> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AnnouncementViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<AnnouncementViewModel>>(data);
            return employee;
        }

        public async Task<AnnouncementViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<AnnouncementViewModel>(employee);
            return employeeVM;
        }

        public async Task<Announcement> Add(AnnouncementViewModel model)
        {
            var userM = _mapper.Map<Announcement>(model);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Announcement> Update(AnnouncementViewModel model)
        {
            var userM = _mapper.Map<Announcement>(model);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Announcement> Delete(int id)
        {
            var data = await _repo.Delete(id);
            return data;
        }
    }
}
