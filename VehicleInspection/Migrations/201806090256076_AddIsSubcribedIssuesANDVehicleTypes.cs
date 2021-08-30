namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSubcribedIssuesANDVehicleTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vehicles", "VehicleTypeId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "IsSubscribedIssues", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropColumn("dbo.AspNetUsers", "IsSubscribedIssues");
            DropColumn("dbo.Vehicles", "VehicleTypeId");
            DropTable("dbo.VehicleTypes");
        }
    }
}
