namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNewColumnCountLike : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GRNewsFeedComments", "CountLike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GRNewsFeedComments", "CountLike");
        }
    }
}
