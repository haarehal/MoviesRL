namespace MoviesRL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'29415713-263c-4f91-996f-d15cbc045e92', N'guest@moviesrl.com', 0, N'AMhLYs2Op+uK4HxDsIGs/Bn2tHvXbcvj5u8aRxJN1z0CtfPc2yC2bNeK9STrVtZK5A==', N'e0faaa70-ad6e-47b9-84d4-e803311de911', NULL, 0, 0, NULL, 1, 0, N'guest@moviesrl.com')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'aeed2ce5-c9b3-4737-936a-8f49ca8065ce', N'admin@moviesrl.com', 0, N'AAFjtnsa9D0fWkTb/6Xt4vNIkqG2E3wK8CcigBCZHs+VF32t9xpEBYW+HMAucbAVbA==', N'2a1ef685-baf9-4771-8a13-212d25f5c332', NULL, 0, 0, NULL, 1, 0, N'admin@moviesrl.com')
                    
                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f823b4ae-a2ca-4afc-bc64-314c41b95413', N'CanManageMovies')
                    
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aeed2ce5-c9b3-4737-936a-8f49ca8065ce', N'f823b4ae-a2ca-4afc-bc64-314c41b95413')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
