using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VUPayRoll.Entities;

namespace Appointment.WebApp.SalaryReport
{
    public class SalarySlip
    {
        public string Allowance { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Days { get; set; }
        public string Salary { get; set; }
        public string BasicSalary { get; set; }


        //Employee Info
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
    }
}