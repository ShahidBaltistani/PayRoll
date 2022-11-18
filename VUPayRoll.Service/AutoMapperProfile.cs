using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using VUPayRoll.Entities;
using VUPayRoll.ViewModel;
using VUPayRoll.ViewModel.Account;

namespace VUPayRoll.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SalaryViewModel, Salary>();
            CreateMap<Salary, SalaryViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<ReligionViewModel, Religion>();
            CreateMap<Religion, ReligionViewModel>();
            CreateMap<City, CityViewModel>();
            CreateMap<CityViewModel, City>();
            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();
            CreateMap<DesignationType, DesignationTypeViewModel>();
            CreateMap<DesignationTypeViewModel, DesignationType>();
            CreateMap<Dependent, DependentViewModel>();
            CreateMap<DependentViewModel, Dependent>();
            CreateMap<RelationshipType, RelationshipTypeViewModel>();
            CreateMap<RelationshipTypeViewModel, RelationshipType>();
            CreateMap<Document, DocumentViewModel>();
            CreateMap<DocumentViewModel, Document>();
            CreateMap<AllowanceType, AllowanceTypeViewModel>();
            CreateMap<AllowanceTypeViewModel, AllowanceType>();
            CreateMap<EmployeeAllowance, EmployeeAllowanceViewModel>();
            CreateMap<EmployeeAllowanceViewModel, EmployeeAllowance>();
            CreateMap<Nationality, NationalityViewModel>();
            CreateMap<NationalityViewModel, Nationality>();
            CreateMap<SignUpViewModel, User>();
            CreateMap<User, SignUpViewModel>();
            CreateMap<LoginViewModel, User>();
            CreateMap<RoleViewModel, Role>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<LeaveViewModel, Leave>();
            CreateMap<Leave, LeaveViewModel>();
            CreateMap<AttendanceViewModel, Attendance>();
            CreateMap<Attendance, AttendanceViewModel>();  
            
            CreateMap<AnnouncementViewModel, Announcement>();
            CreateMap<Announcement, AnnouncementViewModel>(); 
            
            CreateMap<TeamViewModel, Team>();
            CreateMap<Team, TeamViewModel>();

            CreateMap<ProjectNameViewModel, ProjectName>();
            CreateMap<ProjectName, ProjectNameViewModel>();

            CreateMap<ProjectsViewModel, Projects>();
            CreateMap<Projects, ProjectsViewModel>();

            CreateMap<TaskViewModel, TaskT>();
            CreateMap<TaskT, TaskViewModel>();
        }
    }
}
