namespace Practical15Test02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleMasters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleDescription = c.String(nullable: false, maxLength: 20),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        UserId = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Isactive = c.Boolean(nullable: false),
                        Createdon = c.DateTime(nullable: false),
                        RoleMasterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleMasters", t => t.RoleMasterId, cascadeDelete: true)
                .Index(t => t.RoleMasterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMasters", "RoleMasterId", "dbo.RoleMasters");
            DropIndex("dbo.UserMasters", new[] { "RoleMasterId" });
            DropTable("dbo.UserMasters");
            DropTable("dbo.RoleMasters");
        }
    }
}
