using System;
using System.Collections.Generic;
using System.Text;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.Entities
{
   public class Attendance : BaseEntity
    {
        public  AttendanceType AttentanceType { get; set; }
        public DateTime InDate { get; set; }
        public DateTime InTime { get; set; }
        public Nullable<DateTime> OutDate { get; set; }
        public DateTime OutTime { get; set; }
        public string Remarks { get; set; }
        public ApprovalStatus IsApproved { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
