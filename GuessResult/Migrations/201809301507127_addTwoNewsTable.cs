namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addTwoNewsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GRNewsFeeds",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    LikeCount = c.Int(nullable: false),
                    InsertDate = c.DateTime(nullable: false),
                    GRUserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GRUsers", t => t.GRUserId, cascadeDelete: true)
                .Index(t => t.GRUserId);

            CreateTable(
                "dbo.GRNewsFeedComments",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Content = c.String(),
                    IsDeleted = c.Boolean(nullable: false),
                    InsertDate = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.GRNewsFeedComments", "GRUserId", "dbo.GRUsers");
            DropForeignKey("dbo.GRNewsFeedComments", "GRNewsFeedId", "dbo.GRNewsFeeds");
            DropForeignKey("dbo.GRNewsFeeds", "GRUserId", "dbo.GRUsers");
            DropIndex("dbo.GRNewsFeedComments", new[] { "GRUserId" });
            DropIndex("dbo.GRNewsFeedComments", new[] { "GRNewsFeedId" });
            DropIndex("dbo.GRNewsFeeds", new[] { "GRUserId" });
            DropTable("dbo.GRNewsFeedComments");
            DropTable("dbo.GRNewsFeeds");
        }
    }
}
