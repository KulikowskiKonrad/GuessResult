namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GRUserEvents", "IsCorrectPrediction", c => c.Boolean());
            DropColumn("dbo.GRUserEvents", "IsCorectPrediction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GRUserEvents", "IsCorectPrediction", c => c.Boolean());
            DropColumn("dbo.GRUserEvents", "IsCorrectPrediction");
        }
    }
}
