namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImageTitle : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Title", c => c.String());
        }
    }
}
