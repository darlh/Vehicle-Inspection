namespace VehicleInspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedManagerUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [IsManager]) VALUES (N'563a6a99-a951-48a1-80d5-ce8b16e49863', NULL, 0, N'ANYNPjO2WiEuzYVOSawTalF5ursXgdT6fnP+z411pTR2gq9LZSFyQQmCHjcWUbtmJg==', N'4b1491e1-2dbc-405d-95f2-0486c9cd6feb', NULL, 0, 0, NULL, 1, 0, N'admin', N'Manager', 1)

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1d2e4e62-2ced-4d7e-a976-19b1fa95ec6a', N'Manager')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'563a6a99-a951-48a1-80d5-ce8b16e49863', N'1d2e4e62-2ced-4d7e-a976-19b1fa95ec6a')

");
        }
        
        public override void Down()
        {
        }
    }
}
