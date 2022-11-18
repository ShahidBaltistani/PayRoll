using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public EmployeeViewModel Manager { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}
