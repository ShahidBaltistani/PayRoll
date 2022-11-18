using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VUPayRoll.Repository;
using VUPayRoll.Entities;
using VUPayRoll.Service;
using VUPayRoll.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace VUPayRoll.API.Controllers
{
    [Route("Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeServices _service;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EmployeeController(EmployeeServices service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _service.GetAll();
            return Ok(employees);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var employees = await _service.Get(Id);
            return Ok(employees);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeViewModel employeeInfoViewModel)
        {
            var employees = await _service.Add(employeeInfoViewModel);
            return Ok(employees);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync(EmployeeViewModel employeeInfoViewModel)
        {
            var employes = await _service.Update(employeeInfoViewModel);
            return Ok(employes);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employees = await _service.Delete(id);
            return Ok(employees);
        }


        //Custom
        [HttpGet("EmployeeListById/{Id}")]
        public async Task<IActionResult> EmployeeListById(int Id)
        {
            var employees = await _service.EmployeeListById(Id);
            return Ok(employees);
        }

        [HttpGet("getCities")]
        public async Task<IActionResult> GetCities()
        {
            var data = await _service.GetCities();
            return Ok(data);
        } 
        
        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var data = await _service.GetCountries();
            return Ok(data);
        } 
        
        [HttpGet("GetNationalities")]
        public async Task<IActionResult> GetNationalities()
        {
            var data = await _service.GetNationalities();
            return Ok(data);
        }  
        
        [HttpGet("GetReligions")]
        public async Task<IActionResult> GetReligions()
        {
            var data = await _service.GetReligion();
            return Ok(data);
        } 
        
        [HttpGet("GetDesignationType")]
        public async Task<IActionResult> GetDesignationType()
        {
            var data = await _service.GetDesignationType();
            return Ok(data);
        }

        [HttpPost("createTeam")]
        public async Task<IActionResult> CreateTeam(TeamViewModel model)
        {
            var data = await _service.CreateTeam(model);
            return Ok(data);
        } 
        
        [HttpGet("GetTeams")]
        public async Task<IActionResult> GetTeamLeads()
        {
            var data = await _service.GetTeams();
            return Ok(data);
        }



        //Custom File Upload

        [HttpPost("ImageUpload")]
        public async Task<IActionResult> ImageUpload(IFormFile File,int Id)
        {
            string UniqueFileName = null;
            string UploadFolder = Path.Combine(hostingEnvironment.WebRootPath,"images");
            UniqueFileName = Guid.NewGuid().ToString() + "_" + File.FileName;
            string FilePath= Path.Combine(UploadFolder,UniqueFileName);
            File.CopyTo(new FileStream(FilePath,FileMode.Create));

            var data = await _service.UploadImage(UniqueFileName,Id);
            return Ok(data);
        }
    }
}