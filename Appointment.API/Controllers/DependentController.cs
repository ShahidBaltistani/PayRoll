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
    [Route("Dependent")]
    [ApiController]
    public class DependentController : ControllerBase
    {
        private readonly DependentService _service;
        public DependentController(DependentService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]

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
        public async Task<IActionResult> AddAsync([FromBody] DependentViewModel dependentViewModel)
        {
            var employees = await _service.Add(dependentViewModel);
            return Ok(employees);
        }

        [HttpPost("update")]

        public async Task<IActionResult> UpdateAsync(DependentViewModel dependentViewModel)
        {
            var employes = await _service.Update(dependentViewModel);
            return Ok(employes);
        }

        [HttpGet("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employees = await _service.Delete(id);
            return Ok(employees);
        }


        // Custom
        [HttpGet("GetDependentByEmployeeId/{Id}")]
        public async Task<IActionResult> GetDependentByEmployeeId(int Id)
        {
            var employees = await _service.GetDependentByEmployeeId(Id);
            return Ok(employees);
        }

        [HttpGet("GetRelationshipTypes")]
        public async Task<IActionResult> GetRelationshipTypes()
        {
            var data = await _service.GetRelationshipTypes();
            return Ok(data);
        }

    }
}