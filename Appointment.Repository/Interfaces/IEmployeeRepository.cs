using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;

namespace VUPayRoll.Repository.Interfaces
{
   public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> EmployeeListById(int Id);
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<Nationality>> GetNationalities();
        Task<IEnumerable<Religion>> GetReligions();
        Task<IEnumerable<DesignationType>> GetDesignationTypes();
        Task<Team> CreateTeam(Team team);
        Task<IEnumerable<Team>> GetTeams();
        Task<IEnumerable<Employee>> GetAll();
        IEnumerable<Employee> GetAllE();


        //Works with Joins

        Task<IEnumerable<Employee>> GetAllTest();
        Task<Employee> UploadImage(string FilePath, int Id);
    }
}
