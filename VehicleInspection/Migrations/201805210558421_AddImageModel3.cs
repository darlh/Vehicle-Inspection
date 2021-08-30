namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageModel3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImagePath = c.String(),
                        InspectionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Inspections", t => t.InspectionID, cascadeDelete: true)
                .Index(t => t.InspectionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "InspectionID", "dbo.Inspections");
            DropIndex("dbo.Images", new[] { "InspectionID" });
            DropTable("dbo.Images");
        }
    }
}
