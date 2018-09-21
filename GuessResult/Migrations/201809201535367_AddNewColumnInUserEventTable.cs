namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnInUserEventTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GRUserEvents", "IsCorectPrediction", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GRUserEvents", "IsCorectPrediction");
        }
    }
}
