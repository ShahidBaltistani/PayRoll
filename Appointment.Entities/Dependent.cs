using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.Entities
{
    public class Dependent : BaseEntity
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int RelationshipTypeId { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public GenderEnum Gender { get; set; }
        public MaritalStatusEnum MaritalStatus { get; set; }
        public string Mobile { get; set; }
    }
}
