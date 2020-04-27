namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class seedUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2aedf88f-60c5-44db-b100-e846e62bf17d', N'Admin@vidly.com', 0, N'AKPBv81mQZExzXJpZBhGH8CDiGjZGkN4cuzyn567vJDu0gYJwggXhMc3MfpH0n2waA==', N'bb922838-c69d-4bdf-92be-918b8b5892f4', NULL, 0, 0, NULL, 1, 0, N'Admin@vidly.com')");
            Sql(@"INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'6161ebd9-f3ab-4dca-84fe-a0ddbf36242b', N'guest@vdily.com', 0, N'AHoHl7XlI5I8PqTF4NsBXek74Zhna92XCsXi9ejSrjIzS+L7lKeQEklFf6Vd743c8A==', N'56f6ceb2-39cd-4fde-9f38-933960f12bf5', NULL, 0, 0, NULL, 1, 0, N'guest@vdily.com')");

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b61f15f2-eb78-4b11-ae87-7c3ce34acbf5', N'CanManageMovie')");

            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2aedf88f-60c5-44db-b100-e846e62bf17d', N'b61f15f2-eb78-4b11-ae87-7c3ce34acbf5')");
        }

        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
        }
    }
}
