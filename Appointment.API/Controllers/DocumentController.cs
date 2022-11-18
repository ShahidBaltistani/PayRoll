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
    [Route("Document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _service;
        public DocumentController(DocumentService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _service.GetAll();
            return Ok(employees);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var employees = await _service.Get(id);
            return Ok(employees);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] DocumentViewModel documentViewModel)
        {
            var employees = await _service.Add(documentViewModel);
            return Ok(employees);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(DocumentViewModel documentViewModel)
        {
            var employes = await _service.Update(documentViewModel);
            return Ok(employes);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employees = await _service.Delete(id);
            return Ok(employees);
        }

        //Custom

        [HttpGet("GetDocumentByEmployeeId/{Id}")]
        public async Task<IActionResult> GetDocumentByEmployeeId(int Id)
        {
            var employees = await _service.GetDocumentByEmployeeId(Id);
            return Ok(employees);
        }
    }
}