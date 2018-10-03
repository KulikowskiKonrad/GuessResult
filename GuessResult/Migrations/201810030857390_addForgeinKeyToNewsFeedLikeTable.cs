namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForgeinKeyToNewsFeedLikeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", c => c.Long(nullable: false));
            CreateIndex("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId");
            AddForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId", "dbo.GRNewsFeedComments");
            DropIndex("dbo.GRNewsFeedLikes", new[] { "GRNewsFeedCommentId" });
            DropColumn("dbo.GRNewsFeedLikes", "GRNewsFeedCommentId");
        }
    }
}
