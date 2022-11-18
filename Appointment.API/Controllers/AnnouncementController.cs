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
    [Route("announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly AnnouncementService _service;
        public AnnouncementController(AnnouncementService service)
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
            var data = await _service.Get(Id);
            return Ok(data);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AnnouncementViewModel model)
        {
            var data = await _service.Add(model);
            return Ok(data);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync(AnnouncementViewModel model)
        {
            var data = await _service.Update(model);
            return Ok(data);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.Delete(id);
            return Ok(data);
        }
    }
}
