using Practical13Test01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practical13Test01.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name = MyConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Practical13Test01.Migrations.Configuration>());
        }
        public DbSet<Employee>Employees { get; set; }
    }
}