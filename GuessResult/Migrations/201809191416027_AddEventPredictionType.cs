namespace GuessResult.Migrations
{
    using GuessResult.Enum;
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddEventPredictionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GREvents", "PredictionType", c => c.Int(nullable: false, defaultValue: (byte)EventPredictionType.ExactScore));
            AddColumn("dbo.GRUserEvents", "GeneralScoreType", c => c.Int());
            AlterColumn("dbo.GRUserEvents", "HomeTeamScore", c => c.Byte());
            AlterColumn("dbo.GRUserEvents", "AwayTeamScore", c => c.Byte());
        }

        public override void Down()
        {
            AlterColumn("dbo.GRUserEvents", "AwayTeamScore", c => c.Byte(nullable: false));
            AlterColumn("dbo.GRUserEvents", "HomeTeamScore", c => c.Byte(nullable: false));
            DropColumn("dbo.GRUserEvents", "GeneralScoreType");
            DropColumn("dbo.GREvents", "PredictionType");
        }
    }
}
