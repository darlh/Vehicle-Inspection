namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsManager1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsManager", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsManager");
        }
    }
}
