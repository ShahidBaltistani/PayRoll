using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;

namespace VUPayRoll.Repository
{
    public class EmployeeRepository : IVUPayRoll<Employee>,IEmployeeRepository
    {
        private readonly VUPayRollDBContext _context = null;
        public EmployeeRepository(VUPayRollDBContext context)
        {
            this._context = context;
        }
        public async Task<Employee> Get(int Id)
        {
            return await _context.Employees.Include(x => x.Nationality).Include(x=>x.Religions).Include(x=>x.Cities.Country).Include(x=>x.Cities).Include(x=>x.DesignationTypes).Where(x=>x.ID==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.Include(x=>x.Nationality).Include(x => x.Religions).Include(x => x.Cities.Country).Include(x => x.Cities).Include(x => x.DesignationTypes).Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllTest()
        {
            Expression<Func<Employee, bool>> filtr = x => x.IsDelete == false;
            var Result = from e in _context.Employees.Where(filtr)
                         join n in _context.Nationalities on e.NationalityId equals n.Id
                         join r in _context.Religions on e.ReligionId equals r.Id
                         join ci in _context.Cities on e.CityId equals ci.Id
                         join co in _context.Countries on e.Cities.CountryId equals co.Id
                         join de in _context.DesignationTypes on e.DesignationTypeId equals de.Id
                         select new Employee {
                            ID=e.ID,
                            IsDelete=e.IsDelete,
                            CreatedBy=e.CreatedBy,
                            CreatedDate=e.CreatedDate,
                            ModifiedBy=e.ModifiedBy,
                            ModifiedDate=e.ModifiedDate,
                            FirstName=e.FirstName,
                            LastName=e.LastName,
                            CNIC=e.CNIC,
                            DOB=e.DOB,
                            MaritalStatus=e.MaritalStatus,
                            ReligionId=r.Id,
                            ProfileImage=e.ProfileImage,
                            Gender=e.Gender,
                            CityId=ci.Id,
                            NationalityId=n.Id,
                            StreetAddress=e.StreetAddress,
                            OfficialEmail=e.OfficialEmail,
                            AlternateEmail=e.AlternateEmail,
                            Mobile=e.Mobile,
                            HireDate=e.HireDate,
                            JoiningDate=e.JoiningDate,
                            Shift=e.Shift,
                            DesignationTypeId=de.Id,
                            Skill=e.Skill,
                            Department=e.Department,
                            PayType=e.PayType,
                            BasicSalary=e.BasicSalary,
                            SalaryPaymentMethod=e.SalaryPaymentMethod,
                            BankName=e.BankName,
                            AccountNo=e.AccountNo,
                            ProbationPeriod=e.ProbationPeriod,
                            UserId=e.UserId
                         };

            var Data = await Result.ToListAsync();
            return Data;
        }
        public IEnumerable<Employee> GetAllE()
        {
            return _context.Employees.Include(x=>x.Nationality).Include(x => x.Religions).Include(x => x.Cities.Country).Include(x => x.Cities).Include(x => x.DesignationTypes).Where(x => x.IsDelete == false);
        }

        public async Task<Employee> Add(Employee t)
        {
            t.IsDelete = false;
            await _context.Set<Employee>().AddAsync(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<Employee> Update(Employee tUpdation)
        {
            tUpdation.IsDelete = false;
            var customer = _context.Set<Employee>().Attach(tUpdation);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return tUpdation;
        }

        public async Task<Employee> Delete(int id)
        {
            Employee entity = await _context.Set<Employee>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        //Custom
        public async Task<IEnumerable<Employee>> EmployeeListById(int Id)
        {
            return await _context.Employees.Include(x => x.Religions).Include(x => x.Nationality).Include(x => x.Cities.Country).Include(x => x.DesignationTypes).Where(x=>x.UserId==Id && x.IsDelete==false).ToListAsync();
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.Include(x => x.Country).ToListAsync();
        } 
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        } 
        public async Task<IEnumerable<Nationality>> GetNationalities()
        {
            return await _context.Nationalities.ToListAsync();
        }  
        public async Task<IEnumerable<Religion>> GetReligions()
        {
            return await _context.Religions.ToListAsync();
        } 
        public async Task<IEnumerable<DesignationType>> GetDesignationTypes()
        {
            return await _context.DesignationTypes.ToListAsync();
        }
        public async Task<Team> CreateTeam(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.Include(x => x.Employee).ToListAsync();
        }


        //Image Working

        public async Task<Employee> UploadImage(string FilePath,int Id)
        {
            Employee entity = await _context.Set<Employee>().FindAsync(Id);
            if (entity != null)
            {
                entity.ProfileImage = FilePath;
            }
            var employee = _context.Set<Employee>().Attach(entity);
            employee.State =EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
