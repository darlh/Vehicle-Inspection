namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicleLastInspect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "LastInspect", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "LastInspect");
        }
    }
}
