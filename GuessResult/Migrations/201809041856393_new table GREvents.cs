namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtableGREvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GREvents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        HomeTeamName = c.String(nullable: false, maxLength: 100),
                        AwayTeamName = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GREvents");
        }
    }
}
