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

namespace VUPayRoll.API.Controllers
{
    [Route("Leave")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService _service;
        public LeaveController(LeaveService service)
        {
            _service = service;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var leaves = await _service.GetAll();
            return Ok(leaves);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var leaves = await _service.Get(Id);
            return Ok(leaves);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] LeaveViewModel leaveViewModel)
        {
            var leaves = await _service.Add(leaveViewModel);
            return Ok(leaves);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync(LeaveViewModel leaveViewModel)
        {
            var leaves = await _service.Update(leaveViewModel);
            return Ok(leaves);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var leaves = await _service.Delete(id);
            return Ok(leaves);
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
        [HttpGet("GetAllNotApprovedLeaveForAdminDashboard")]
        public async Task<IActionResult> GetAllNotApprovedLeaveForAdminDashboard()
        {
            var leaves = await _service.GetAllNotApprovedLeaveForAdminDashboard();
            return Ok(leaves);
        }
        [HttpGet("GetAllByUserId/{UserId}")]
        public async Task<IActionResult> GetAllByUserId(int UserId)
        {
            var leaves = await _service.GetAllByUserId(UserId);
            return Ok(leaves);
        }

    }
}