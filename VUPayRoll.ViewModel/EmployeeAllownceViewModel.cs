using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
    public class EmployeeAllowanceViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public int AllowanceId { get; set; }
        public AllowanceTypeViewModel Allowance { get; set; }
        public string Amount { get; set; }
    }
}
