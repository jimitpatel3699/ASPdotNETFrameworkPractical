using Practical15Test02.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practical15Test02.Data
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext() : base("name = MyConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Practical15Test02.Migrations.Configuration>());
        }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<UserMaster>UserMasters { get; set; }
    }
}