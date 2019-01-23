namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNewTableFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GRFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OriginalFileName = c.String(),
                        GeneratedFileName = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GRNewsFeeds", "GRFileId", c => c.Long());
            CreateIndex("dbo.GRNewsFeeds", "GRFileId");
            AddForeignKey("dbo.GRNewsFeeds", "GRFileId", "dbo.GRFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GRNewsFeeds", "GRFileId", "dbo.GRFiles");
            DropIndex("dbo.GRNewsFeeds", new[] { "GRFileId" });
            DropColumn("dbo.GRNewsFeeds", "GRFileId");
            DropTable("dbo.GRFiles");
        }
    }
}
