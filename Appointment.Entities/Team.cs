using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Entities
{
   public class Team :BaseEntity
    {
        public int ManagerId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
