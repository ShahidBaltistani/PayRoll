using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Entities
{
    public class ProjectName:BaseEntity
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string Language { get; set; }
        public string TFSStatusLastUpdated { get; set; }
        public string TeamLead { get; set; }
    }
}
