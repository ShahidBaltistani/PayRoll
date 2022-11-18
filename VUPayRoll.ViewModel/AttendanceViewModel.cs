using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.ViewModel.Account;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.ViewModel
{
   public class AttendanceViewModel
    {
        public int Id { get; set; }
        public AttendanceType AttentanceType { get; set; }
        public DateTime InDate { get; set; }
        public DateTime InTime { get; set; }
        public Nullable<DateTime> OutDate { get; set; }
        public DateTime OutTime { get; set; }
        public string Remarks { get; set; }
        public ApprovalStatus IsApproved { get; set; }
        public int UserId { get; set; }
        public virtual SignUpViewModel User { get; set; }
    }
}
