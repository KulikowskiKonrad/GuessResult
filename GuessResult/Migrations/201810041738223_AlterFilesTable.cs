namespace GuessResult.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFilesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GRFiles", "OriginalFileName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.GRFiles", "GeneratedFileName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.GRFiles", "Extension", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GRFiles", "Extension", c => c.String());
            AlterColumn("dbo.GRFiles", "GeneratedFileName", c => c.String());
            AlterColumn("dbo.GRFiles", "OriginalFileName", c => c.String());
        }
    }
}
