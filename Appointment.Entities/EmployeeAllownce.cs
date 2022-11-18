using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUPayRoll.Entities
{
    public class EmployeeAllowance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int AllowanceId { get; set; }
        public AllowanceType Allowance { get; set; }
        public string Amount { get; set; }
    }
}
