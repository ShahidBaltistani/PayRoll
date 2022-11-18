using System;
using System.Collections.Generic;
using System.Text;
using static VUPayRoll.ViewModel.Enumerations;

namespace VUPayRoll.ViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int ProjId { get; set; }
        public int EmpId { get; set; }
        public TaskType TaskType { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
