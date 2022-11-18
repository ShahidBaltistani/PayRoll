using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VUPayRoll.Service;
using VUPayRoll.ViewModel;

namespace VUPayRoll.API.Controllers
{
    [Route("Salary")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly SalaryService _service;
       
        public SalaryController(SalaryService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetSalarySlips()
        {
            var salaries = await _service.GetAllSalarySlip();
            return Ok(salaries);
        }
        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var salary = await _service.Get(Id);
            return Ok(salary);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(SalaryViewModel salarySViewModel)
        {
            var salary = await _service.Add(salarySViewModel);
            return Ok(salary);
        }
    }
}