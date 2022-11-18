using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VUPayRoll.Entities;
using VUPayRoll.ViewModel;
using static VUPayRoll.ViewModel.Enumerations;

namespace VUPayRoll.WebApp.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public DateTime? DOB { get; set; }
        public MaritalStatusEnum MaritalStatus { get; set; }
        public int ReligionId { get; set; }
        public virtual Religion Religions { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public GenderEnum Gender { get; set; }
        public int CityId { get; set; }
        public virtual CityViewModel Cities { get; set; }
        public int NationalityId { get; set; }
        public NationalityViewModel Nationality { get; set; }
        public string StreetAddress { get; set; }
        public string OfficialEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string Mobile { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public ShiftEnum Shift { get; set; }
        public int DesignationTypeId { get; set; }
        public virtual DesignationType DesignationTypes { get; set; }
        public string Skill { get; set; }
        public Department Department { get; set; }
        public PayTypeEnum PayType { get; set; }
        public string BasicSalary { get; set; }
        public SalaryPaymentMethodEnum SalaryPaymentMethod { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public bool IsDeleted { get; set; }
        public string ProbationPeriod { get; set; }
        public int UserId { get; set; }
        public virtual VUPayRoll.ViewModel.Account.SignUpViewModel User { get; set; }
    }
}