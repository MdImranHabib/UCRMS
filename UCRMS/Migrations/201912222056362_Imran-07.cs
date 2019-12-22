namespace UCRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imran07 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "RegNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "RegNo", c => c.String(nullable: false));
        }
    }
}
