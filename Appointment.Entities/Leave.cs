using System;
using System.Collections.Generic;
using System.Text;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.Entities
{
    public class Leave:BaseEntity
    {
        public LeaveType LeaveType { get; set; }
        public string DateRange { get; set; }
        public string Description { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
        public LeavehfDay LeavehfDay { get; set; }
        public DateTime? Date { get; set; }
        public ApprovalStatus IsApproved { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
