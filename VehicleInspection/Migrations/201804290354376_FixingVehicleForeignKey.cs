namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingVehicleForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inspections", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Inspections", new[] { "Vehicle_Id" });
            DropColumn("dbo.Inspections", "VehicleId");
            RenameColumn(table: "dbo.Inspections", name: "Vehicle_Id", newName: "VehicleId");
            AlterColumn("dbo.Inspections", "VehicleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Inspections", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Inspections", "VehicleId");
            AddForeignKey("dbo.Inspections", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspections", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Inspections", new[] { "VehicleId" });
            AlterColumn("dbo.Inspections", "VehicleId", c => c.Int());
            AlterColumn("dbo.Inspections", "VehicleId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Inspections", name: "VehicleId", newName: "Vehicle_Id");
            AddColumn("dbo.Inspections", "VehicleId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Inspections", "Vehicle_Id");
            AddForeignKey("dbo.Inspections", "Vehicle_Id", "dbo.Vehicles", "Id");
        }
    }
}
