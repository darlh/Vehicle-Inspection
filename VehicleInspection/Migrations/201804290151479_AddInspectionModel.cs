namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInspectionModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VehicleModels", newName: "Vehicles");
            CreateTable(
                "dbo.Inspections",
                c => new
                    {
                        InspectionID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Mileage = c.Int(nullable: false),
                        Tires = c.Boolean(nullable: false),
                        Brakes = c.Boolean(nullable: false),
                        Fluids = c.Boolean(nullable: false),
                        Lights = c.Boolean(nullable: false),
                        Exterior = c.Boolean(nullable: false),
                        Interior = c.Boolean(nullable: false),
                        Supplies = c.Boolean(nullable: false),
                        Notes = c.String(),
                        Issues = c.String(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.InspectionID)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Vehicle_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspections", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Inspections", new[] { "Vehicle_Id" });
            DropTable("dbo.Inspections");
            RenameTable(name: "dbo.Vehicles", newName: "VehicleModels");
        }
    }
}
