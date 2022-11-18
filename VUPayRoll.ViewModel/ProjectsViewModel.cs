using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
    public class ProjectsViewModel
    {
        public int Id { get; set; }
        public int ProjectNameId { get; set; }
        public ProjectNameViewModel ProjectName { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}
