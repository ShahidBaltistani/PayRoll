using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Entities
{
    public class Projects:BaseEntity
    {
        public int ProjectNameId { get; set; }
        public virtual ProjectName ProjectName { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
