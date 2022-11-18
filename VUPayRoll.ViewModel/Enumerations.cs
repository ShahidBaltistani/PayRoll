using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
    public class Enumerations
    {
        public enum GenderEnum
        {
            Male,
            Female
        }
        public enum ShiftEnum
        {
            full_time,
            part_time
        }
        public enum MaritalStatusEnum
        {
            Single,
            Married
        }
        public enum PayTypeEnum
        {
            Weekly,
            Monthly,
            Yearly
        }
        public enum SalaryPaymentMethodEnum
        {
            Bank_Transfer,
            Cash,
            by_cheque
        }
        public enum Department
        {
            Developement_Department,
            Administration_Department,
            HR_Department,
            SQA_Department
        }
        public enum LeaveType
        {
            Marriage_Leave,
            Sick_Leave,
            Ummrah_Leave,
            Hajj_Leave,
            Other
        }
        public enum LeaveStatus
        {
            Yes,
            No
        }
        public enum LeavehfDay
        {
            Full_Day,
            Half_Day
        }
        public enum AttendanceType
        {
            Manual,
            Official_Visit,
            Travel,
            Training

        }
        public enum ApprovalStatus
        {
            InProcess,
            Approved,
            Rejected
        }
        public enum TaskType
        {
            Manage,
            Development,
            Designing
        }
    }
}

