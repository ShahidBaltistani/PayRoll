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
    public class EmployeeServices
    {

        private readonly IVUPayRoll<Employee> _repo;
        private readonly IEmployeeRepository _repo2;
        private readonly IMapper _mapper;

        public EmployeeServices(IVUPayRoll<Employee> repo, IEmployeeRepository repo2, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAll()
        {
            var data =await _repo2.GetAllTest();
            var employee =_mapper.Map<IEnumerable<EmployeeViewModel>>(data);
            return employee;
        }

        public async Task<EmployeeViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);
            return employeeVM;
        }


        public async Task<Employee> Add(EmployeeViewModel employeeInfoViewModel)
        {
            var userM = _mapper.Map<Employee>(employeeInfoViewModel);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Employee> Update(EmployeeViewModel employeeInfoView)
        {
            var userM = _mapper.Map<Employee>(employeeInfoView);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Employee> Delete(int id)
        {
            var employeeInfo = await _repo.Delete(id);
            return employeeInfo;
        }


        public async Task<IEnumerable<EmployeeViewModel>> EmployeeListById(int Id)
        {
            var data = await _repo2.EmployeeListById(Id);
            var employee = _mapper.Map<IEnumerable<EmployeeViewModel>>(data);
            return employee;
        }

        public async Task<IEnumerable<CityViewModel>> GetCities()
        {
            var data = await _repo2.GetCities();
            var result = _mapper.Map<IEnumerable<CityViewModel>>(data);
            return result;
        } 
        
        public async Task<IEnumerable<CountryViewModel>> GetCountries()
        {
            var data = await _repo2.GetCountries();
            var result = _mapper.Map<IEnumerable<CountryViewModel>>(data);
            return result;
        } 
        public async Task<IEnumerable<NationalityViewModel>> GetNationalities()
        {
            var data = await _repo2.GetNationalities();
            var result = _mapper.Map<IEnumerable<NationalityViewModel>>(data);
            return result;
        } 
        
        public async Task<IEnumerable<ReligionViewModel>> GetReligion()
        {
            var data = await _repo2.GetReligions();
            var result = _mapper.Map<IEnumerable<ReligionViewModel>>(data);
            return result;
        } 
        public async Task<IEnumerable<DesignationTypeViewModel>> GetDesignationType()
        {
            var data = await _repo2.GetDesignationTypes();
            var result = _mapper.Map<IEnumerable<DesignationTypeViewModel>>(data);
            return result;
        }

        public async Task<Team> CreateTeam(TeamViewModel model)
        {
            var userM = _mapper.Map<Team>(model);
            var res = await _repo2.CreateTeam(userM);
            return res;
        }

        public async Task<IEnumerable<TeamViewModel>> GetTeams()
        {
            var data = await _repo2.GetTeams();
            var result = _mapper.Map<IEnumerable<TeamViewModel>>(data);
            return result;
        }



        //Image Working
        public async Task<Employee> UploadImage(string FilePath, int Id)
        {
            var employeeInfo = await _repo2.UploadImage(FilePath,Id);
            return employeeInfo;
        }
    }
}
