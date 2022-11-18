using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.ViewModel.Account;
using static VUPayRoll.ViewModel.Enumerations;

namespace VUPayRoll.ViewModel
{
    public class DependentViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public int RelationshipTypeId { get; set; }
        public virtual RelationshipTypeViewModel RelationshipType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public GenderEnum Gender { get; set; }
        public MaritalStatusEnum MaritalStatus { get; set; }
        public string Mobile { get; set; }
    }
}
