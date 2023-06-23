using Practical13Test02.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practical13Test02.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext() : base("name = MyConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Practical13Test02.Migrations.Configuration>());
        }
       public DbSet<EmployeeDesignation> EmployeeDesignations { get; set; }
       public DbSet<Employee> Employees { get; set; }
    }
}