using System;
using System.Collections.Generic;
using System.Text;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.Entities
{
    public class TaskT:BaseEntity
    {
        public int ProjId { get; set; }
        public int EmpId { get; set; }
        public TaskType TaskType { get; set; }
        public string Description { get; set; }
    }
}
