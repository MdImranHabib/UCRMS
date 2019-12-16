namespace UCRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imran02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseAssigns", "IsAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseAssigns", "IsAssigned");
        }
    }
}
