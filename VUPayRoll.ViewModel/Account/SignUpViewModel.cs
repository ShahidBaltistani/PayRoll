using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel.Account
{
   public class SignUpViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public virtual RoleViewModel Role { get; set; }

    }
}
