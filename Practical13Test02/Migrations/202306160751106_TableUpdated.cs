namespace Practical13Test02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeDesignations", "Designation", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeDesignations", "Designation", c => c.String(maxLength: 50));
        }
    }
}
