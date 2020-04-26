namespace UCRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imran10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Day = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId);
            
            DropTable("dbo.Days");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ClassSchedules", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassSchedules", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassSchedules", "CourseId", "dbo.Courses");
            DropIndex("dbo.ClassSchedules", new[] { "RoomId" });
            DropIndex("dbo.ClassSchedules", new[] { "CourseId" });
            DropIndex("dbo.ClassSchedules", new[] { "DepartmentId" });
            DropTable("dbo.ClassSchedules");
        }
    }
}
