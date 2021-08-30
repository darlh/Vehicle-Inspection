namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedVehicleTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[VehicleTypes] ([Id], [Type]) VALUES (1, N'AMB & WC')
INSERT INTO [dbo].[VehicleTypes] ([Id], [Type]) VALUES (2, N'WCP & DBL')
INSERT INTO [dbo].[VehicleTypes] ([Id], [Type]) VALUES (3, N'Combo')
");
        }
        
        public override void Down()
        {
        }
    }
}
