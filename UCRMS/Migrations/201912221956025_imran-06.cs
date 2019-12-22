namespace UCRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imran06 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "ContactNo", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "ContactNo", c => c.String(nullable: false));
        }
    }
}
