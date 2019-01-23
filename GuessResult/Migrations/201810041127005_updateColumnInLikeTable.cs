namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateColumnInLikeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedId", "dbo.GRNewsFeeds");
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments");
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedId" });
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedCommentId" });
            AlterColumn("dbo.GRNewsFeedLikes", "GRNewsFeedId", c => c.Long());
            AlterColumn("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", c => c.Long());
            CreateIndex("dbo.GRNewsFeedLikes", "GRNewsFeedId");
            CreateIndex("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId");
            AddForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedId", "dbo.GRNewsFeeds", "Id");
            AddForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments");
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedId", "dbo.GRNewsFeeds");
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedCommentId" });
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedId" });
            AlterColumn("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", c => c.Long(nullable: false));
            AlterColumn("dbo.GRNewsFeedLikes", "GRNewsFeedId", c => c.Long(nullable: false));
            CreateIndex("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId");
            CreateIndex("dbo.GRNewsFeedLikes", "GRNewsFeedId");
            AddForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedId", "dbo.GRNewsFeeds", "Id", cascadeDelete: true);
        }
    }
}
