namespace Practical13Test02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeDesignations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designation = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        Address = c.String(maxLength: 100),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeDesignationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeDesignations", t => t.EmployeeDesignationId, cascadeDelete: true)
                .Index(t => t.EmployeeDesignationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "EmployeeDesignationId", "dbo.EmployeeDesignations");
            DropIndex("dbo.Employees", new[] { "EmployeeDesignationId" });
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeDesignations");
        }
    }
}
