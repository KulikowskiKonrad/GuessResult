namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreColumnsInGREvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GREvents", "AwayTeamScore", c => c.Byte());
            AddColumn("dbo.GREvents", "HomeTeamScore", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GREvents", "HomeTeamScore");
            DropColumn("dbo.GREvents", "AwayTeamScore");
        }
    }
}
