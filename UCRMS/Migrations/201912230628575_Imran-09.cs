namespace UCRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imran09 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseEnrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrollDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.GradeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Results", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Results", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseEnrolls", "StudentId", "dbo.Students");
            DropForeignKey("dbo.CourseEnrolls", "CourseId", "dbo.Courses");
            DropIndex("dbo.Results", new[] { "GradeId" });
            DropIndex("dbo.Results", new[] { "CourseId" });
            DropIndex("dbo.Results", new[] { "StudentId" });
            DropIndex("dbo.CourseEnrolls", new[] { "CourseId" });
            DropIndex("dbo.CourseEnrolls", new[] { "StudentId" });
            DropTable("dbo.Results");
            DropTable("dbo.CourseEnrolls");
        }
    }
}
