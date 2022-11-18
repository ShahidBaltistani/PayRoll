using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
    public class ProjectsTasks
    {
        public IEnumerable<ProjectsViewModel> Projects { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
