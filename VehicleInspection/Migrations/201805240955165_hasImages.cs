namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspections", "HasImages", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inspections", "HasImages");
        }
    }
}
