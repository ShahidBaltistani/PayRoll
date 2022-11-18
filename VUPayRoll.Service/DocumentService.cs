using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;
using VUPayRoll.Repository;
using VUPayRoll.ViewModel;
using VUPayRoll.Repository.Interfaces;

namespace VUPayRoll.Service
{
    public class DocumentService
    {
        private readonly IVUPayRoll<Document> _repo;
        private readonly IDocumentRepository _repo2;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository repo2, IVUPayRoll<Document> repo,  IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _repo2 = repo2;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var employee = _mapper.Map<IEnumerable<DocumentViewModel>>(data);
            return employee;
        }

        public async Task<DocumentViewModel> Get(int id)
        {
            var employee = await _repo.Get(id);
            var employeeVM = _mapper.Map<DocumentViewModel>(employee);
            return employeeVM;
        }

        public async Task<Document> Add(DocumentViewModel documentViewModel)
        {
            var userM = _mapper.Map<Document>(documentViewModel);
            var employeeInfo = await _repo.Add(userM);
            return employeeInfo;
        }

        public async Task<Document> Update(DocumentViewModel documentViewModel)
        {
            var userM = _mapper.Map<Document>(documentViewModel);
            var employeeInfo = await _repo.Update(userM);
            return employeeInfo;
        }

        public async Task<Document> Delete(int id)
        {
            var employeeInfo = await _repo.Delete(id);
            return employeeInfo;
        }

        //Custom Code
        public async Task<IEnumerable<DocumentViewModel>> GetDocumentByEmployeeId(int Id)
        {
            var data = await _repo2.GetDocumentByEmployeeId(Id);
            var employee = _mapper.Map<IEnumerable<DocumentViewModel>>(data);
            return employee;
        }

    }
}
