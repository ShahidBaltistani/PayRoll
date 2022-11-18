using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VUPayRoll.WebApp.Models
{
    public class EmployeeTaskAddDetail
    {
        public int PId { get; set; }
        public int EmpId { get; set; }
        public string Task { get; set; }
    }
}