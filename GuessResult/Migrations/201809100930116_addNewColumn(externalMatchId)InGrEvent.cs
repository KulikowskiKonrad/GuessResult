namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumnexternalMatchIdInGrEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GREvents", "ExternalMatchId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GREvents", "ExternalMatchId");
        }
    }
}
