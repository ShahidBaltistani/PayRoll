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
    public class SalaryService
    {
        private readonly IVUPayRoll<Salary> _repo;
        private readonly ISalaryRepository _repo2;
        private readonly IMapper _mapper;

        public SalaryService(IVUPayRoll<Salary> repo, IMapper mapper, ISalaryRepository repo2)
        {
            _repo = repo;
            _repo2 = repo2;
            _mapper = mapper;
        }
        public async Task<SalaryViewModel> Get(int Id)
        {
            var salary = await _repo.Get(Id);
            var salaryVM = _mapper.Map<SalaryViewModel>(salary);
            return salaryVM;
        }

        public async Task<Salary> Add(SalaryViewModel salarySViewModel)
        {
            var salary = _mapper.Map<Salary>(salarySViewModel);
            var salaryVM = await _repo.Add(salary);
            return salaryVM;
        }
        public async Task<IEnumerable<SalaryViewModel>> GetAllSalarySlip()
        {
            var data =await _repo2.GetAll();
            var salaryS = _mapper.Map<IEnumerable<SalaryViewModel>>(data);
            return salaryS;
        }
    }
}
