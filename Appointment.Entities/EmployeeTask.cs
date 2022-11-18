using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Entities
{
    public class EmployeeTask
    {
        public int ProjectsId { get; set; }
        public Projects Projects { get; set; }
        public int TaskId { get; set; }
        public TaskT Task { get; set; }
    }
}
