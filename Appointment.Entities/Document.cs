using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUPayRoll.Entities
{
    public class Document : BaseEntity
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
    }
}
