using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.ViewModel
{
    public class ProfileManagementViewModel
    {
        public IEnumerable<SignUpViewModel> Users { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}
