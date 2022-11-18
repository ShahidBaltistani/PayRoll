using VUPayRoll.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.Database
{
    public class VUPayRollDBContext : DbContext
    {
        public VUPayRollDBContext(DbContextOptions<VUPayRollDBContext> options) 
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<DesignationType> DesignationTypes { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<AllowanceType> AllowanceTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<ProjectName> ProjectNames { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<TaskT> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

        }
    }
}
