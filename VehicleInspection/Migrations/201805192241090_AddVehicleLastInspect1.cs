namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicleLastInspect1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "LastInspect", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "LastInspect", c => c.DateTime(nullable: false));
        }
    }
}
