using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.ViewModel
{
    public class DashboardViewModel
    {
        public IEnumerable<SignUpViewModel> Users { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public IEnumerable<LeaveViewModel> leaves { get; set; }
        public IEnumerable<AttendanceViewModel> Attendances { get; set; }
        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }
    }
}
