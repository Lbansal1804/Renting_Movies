namespace RentingMovies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenerals : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Generals (Id, Name) VALUES (1, 'Horror')");
            Sql("INSERT INTO Generals (Id, Name) VALUES (2, 'Action')");
            Sql("INSERT INTO Generals (Id, Name) VALUES (3, 'Family')");
            Sql("INSERT INTO Generals (Id, Name) VALUES (4, 'Romance')");
            Sql("INSERT INTO Generals (Id, Name) VALUES (5, 'Comedy')");
        }
        
        public override void Down()
        {
        }
    }
}
