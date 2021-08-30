namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWindshield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspections", "Windshield", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inspections", "Windshield");
        }
    }
}
