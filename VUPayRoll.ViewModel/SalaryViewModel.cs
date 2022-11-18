using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VUPayRoll.Entities;

namespace VUPayRoll.ViewModel
{
    public class SalaryViewModel
    {
        public int Id { get; set; }
        public string Allowance { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Days { get; set; }
        public string TotalSalary { get; set; }
        public string BasicSalary { get; set; }


        //Link
        public int EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
    }
}