using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VUPayRoll.Entities;
using VUPayRoll.Repository;
using VUPayRoll.Service;
using VUPayRoll.ViewModel;

namespace VUPayRoll.API.Controllers
{
    [Route("EmployeeAllowance")]
    [ApiController]
    public class EmployeeAllowanceController : ControllerBase 
    {
        private readonly EmployeeAllowanceService _service;
        public EmployeeAllowanceController(EmployeeAllowanceService service)
        {
            _service = service;
        }

        [HttpGet("getall")]

        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _service.GetAll();
            return Ok(employees);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var employees = await _service.Get(id);
            return Ok(employees);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeAllowanceViewModel employeeAllowanceViewModel)
        {
            var employees = await _service.Add(employeeAllowanceViewModel);
            return Ok(employees);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(EmployeeAllowanceViewModel employeeAllowanceViewModel)
        {
            var employes = await _service.Update(employeeAllowanceViewModel);
            return Ok(employes);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employees = await _service.Delete(id);
            return Ok(employees);
        }

        //Custom

        [HttpGet("GetEmployeeAllowanceByEmployeeId/{Id}")]
        public async Task<IActionResult> GetEmployeeAllowanceByEmployeeId(int Id)
        {
            var employees = await _service.GetEmployeeAllowanceByEmployeeId(Id);
            return Ok(employees);
        }


        [HttpGet("GetAllowancTypes")]
        public async Task<IActionResult> GetEmployeeAllowances()
        {
            var data = await _service.GetAllowancTypes();
            return Ok(data);
        }
    }
}