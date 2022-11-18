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
    [Route("ProjectManagement")]
    [ApiController]
    public class ProjectManagementController : ControllerBase
    {
        private readonly ProjectManagementService _service;
        public ProjectManagementController(ProjectManagementService service)
        {
            _service = service;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var leaves = await _service.Get(Id);
            return Ok(leaves);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(ProjectNameViewModel projectName)
        {
            var pn = await _service.Add(projectName);
            return Ok(pn);
        }
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.Delete(id);
            return Ok(data);
        }

        //Custom

        [HttpPost("createProjects")]
        public async Task<IActionResult> createProjects(ProjectsViewModel model)
        {
            var data = await _service.CreateProjects(model);
            return Ok(data);
        }
        [HttpGet("getallProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var data = await _service.GetAllProjects();
            return Ok(data);
        }
        [HttpDelete("DeleteProject/{PId}")]
        public async Task<IActionResult> DeleteProject(int PId)
        {
            var data = await _service.DeleteProject(PId);
            return Ok(data);
        }
        [HttpPost("EmployeeTask")]
        public async Task<IActionResult> EmployeeTask(TaskViewModel model)
        {
            var data = await _service.EmployeeTask(model);
            return Ok(data);
        }
        [HttpGet("Tasks")]
        public async Task<IActionResult> Tasks()
        {
            var data = await _service.GetAllTasks();
            return Ok(data);
        }
        [HttpGet("DeleteTask/{Id}/{EmpId}/{PId}")]
        public async Task<IActionResult> Tasks(int Id,int EmpId, int PId)
        {
            var data = await _service.DeleteTask(Id,EmpId, PId);
            return Ok(data);
        }
    }
}