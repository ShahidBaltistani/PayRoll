using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VUPayRoll.Entities;

namespace VUPayRoll.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nationality>().HasData(
                    new Nationality
                    {
                        Id = 1,
                        Name = "Pakistani",
                    }
                );
            modelBuilder.Entity<Religion>().HasData(
                    new Religion
                    {
                        Id = 1,
                        Name = "Muslim",
                    },
                    new Religion
                    {
                        Id = 2,
                        Name = "Non Muslim",
                    }
                );
            modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Admin",
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "User",
                    },
                    new Role
                    {
                        Id = 3,
                        Name = "TeamLead",
                    }
                );
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        ID = 1,
                        Username = "admin",
                        Password="111111",
                        IsApproved=true,
                        RoleId=1

                    }
                );
            modelBuilder.Entity<RelationshipType>().HasData(
                    new RelationshipType
                    {
                        Id=1,
                        Name= "Brother"
                    }
                );
            modelBuilder.Entity<DesignationType>().HasData(
                    new DesignationType
                    {
                        Id = 1,
                        Name = "Software developer"
                    },
                    new DesignationType
                    {
                        Id = 2,
                        Name = "Software Engineer"
                    },
                    new DesignationType
                    {
                        Id = 3,
                        Name = "Junior Software Developer"
                    },
                    new DesignationType
                    {
                        Id = 4,
                        Name = "Junior Software Engineer"
                    },
                    new DesignationType
                    {
                        Id = 5,
                        Name = "Project consultant"
                    },
                    new DesignationType
                    {
                        Id = 6,
                        Name = "Machine learning (Data Analyst)"
                    },
                    new DesignationType
                    {
                        Id = 7,
                        Name = "Admin Officer"
                    },
                    new DesignationType
                    {
                        Id = 8,
                        Name = "HR Executive"
                    },
                    new DesignationType
                    {
                        Id = 9,
                        Name = "Gm Operations"
                    },
                    new DesignationType
                    {
                        Id = 10,
                        Name = "Office Boy"
                    },
                    new DesignationType
                    {
                        Id = 11,
                        Name = "Internee"
                    },
                    new DesignationType
                    {
                        Id = 12,
                        Name = "System Analysts"
                    }
                );
            modelBuilder.Entity<AllowanceType>().HasData(
                   new AllowanceType
                   {
                       Id = 1,
                       Name = "Parking"
                   }
               );
            modelBuilder.Entity<Country>().HasData(
                   new Country
                   {
                       Id = 1,
                       Name = "Pakistan"
                   },
                   new Country
                   {
                       Id = 2,
                       Name = "USA"
                   }
               );
            modelBuilder.Entity<City>().HasData(
                   new City
                   {
                       Id = 1,
                       Name = "Lahore",
                       CountryId=1
                   }
               );
        }
    }
}
