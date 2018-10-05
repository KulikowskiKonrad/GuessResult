namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumnCommentCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GRNewsFeeds", "CommentCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GRNewsFeeds", "CommentCount");
        }
    }
}
