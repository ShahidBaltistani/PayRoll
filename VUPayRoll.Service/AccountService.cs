using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VUPayRoll.Entities;
using VUPayRoll.Repository.Interfaces;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.Service
{
   public class AccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<User> SignUp(SignUpViewModel model)
        {
            var data = _mapper.Map<User>(model);
            var result = await _repo.SignUp(data);
            return result;
        }
        public async Task<User> Login(string username, string password)
        {
            var result = await _repo.Login(username, password);
            return result;
        }
        public async Task<bool> UserExist(string username)
        {
            var result = await _repo.UserExist(username);
            return result;
        } 
        public async Task<SignUpViewModel> Approved(int id)
        {
            var data = await _repo.Approved(id);
            var  result = _mapper.Map<SignUpViewModel>(data);
            return result;
        }
        public async Task<IEnumerable<SignUpViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var users = _mapper.Map<IEnumerable<SignUpViewModel>>(data);
            return users;
        }

        //Custom
        public async Task<IEnumerable<SignUpViewModel>> GetAlls()
        {
            var data = await _repo.GetAlls();
            var users = _mapper.Map<IEnumerable<SignUpViewModel>>(data);
            return users;
        }
    }
}
