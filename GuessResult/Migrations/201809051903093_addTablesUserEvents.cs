namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTablesUserEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GRUserEvents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        EventId = c.Long(nullable: false),
                        HomeTeamScore = c.Byte(nullable: false),
                        AwayTeamScore = c.Byte(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GREvents", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.GRUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GRUserEvents", "UserId", "dbo.GRUsers");
            DropForeignKey("dbo.GRUserEvents", "EventId", "dbo.GREvents");
            DropIndex("dbo.GRUserEvents", new[] { "EventId" });
            DropIndex("dbo.GRUserEvents", new[] { "UserId" });
            DropTable("dbo.GRUserEvents");
        }
    }
}
