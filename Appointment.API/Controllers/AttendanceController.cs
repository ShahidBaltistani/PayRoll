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
    [Route("attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
       
        private readonly AttendanceService _service;
        public AttendanceController(AttendanceService service)
        {
            _service = service;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var attendance = await _service.GetAll();
            return Ok(attendance);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var attendance = await _service.Get(Id);
            return Ok(attendance);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AttendanceViewModel model)
        {
            var attendance = await _service.Add(model);
            return Ok(attendance);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync(AttendanceViewModel model)
        {
            var attendance = await _service.Update(model);
            return Ok(attendance);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var attendance = await _service.Delete(id);
            return Ok(attendance);
        }
        [HttpGet("Rejected/{id}")]
        public async Task<IActionResult> Rejected(int id)
        {
            var user = await _service.Rejected(id);
            return Ok(user);
        }

        [HttpGet("Approved/{id}")]
        public async Task<IActionResult> Approved(int id)
        {
            var user = await _service.Approved(id);
            return Ok(user);
        }
        [HttpGet("GetAllByUserId/{UserId}")]
        public async Task<IActionResult> GetAllByUserId(int UserId)
        {
            var attendance = await _service.GetAllByUserId(UserId);
            return Ok(attendance);
        }


        [HttpGet("NotApprovedList")]
        public async Task<IActionResult> NotApprovedList()
        {
            var notApprovedUsers = await _service.NotApproved();
            return Ok(notApprovedUsers);
        }

    }
}
