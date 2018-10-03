namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNewTableNewsFeedLike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GRNewsFeedLikes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GRNewsFeedId = c.Long(nullable: false),
                        GRUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GRNewsFeeds", t => t.GRNewsFeedId, cascadeDelete: false)
                .ForeignKey("dbo.GRUsers", t => t.GRUserId, cascadeDelete: false)
                .Index(t => t.GRNewsFeedId)
                .Index(t => t.GRUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GRNewsFeedLikes", "GRUserId", "dbo.GRUsers");
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedId", "dbo.GRNewsFeeds");
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRUserId" });
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedId" });
            DropTable("dbo.GRNewsFeedLikes");
        }
    }
}
