namespace RentingMovies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGeneral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Generals",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "GeneralId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "GeneralId");
            AddForeignKey("dbo.Movies", "GeneralId", "dbo.Generals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GeneralId", "dbo.Generals");
            DropIndex("dbo.Movies", new[] { "GeneralId" });
            DropColumn("dbo.Movies", "GeneralId");
            DropTable("dbo.Generals");
        }
    }
}
