using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.Entities;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.ViewModel
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
    }
}
