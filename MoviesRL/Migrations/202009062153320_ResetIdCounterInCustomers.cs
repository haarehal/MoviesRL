namespace MoviesRL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetIdCounterInCustomers : DbMigration
    {
        public override void Up()
        {
            //Sql("DBCC CHECKIDENT ('Customers', RESEED, 1)");
            Sql("TRUNCATE TABLE Customers");
        }
        
        public override void Down()
        {
        }
    }
}
