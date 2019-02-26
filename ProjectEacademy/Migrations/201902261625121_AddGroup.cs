namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserClasses",
                c => new
                    {
                        ClassID = c.String(nullable: false, maxLength: 128),
                        ClassName = c.String(),
                        SubjectName = c.String(),
                        Color = c.String(),
                        TeacherName_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherName_Id)
                .Index(t => t.TeacherName_Id);
            
            CreateTable(
                "dbo.UserInClasses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClassID_ClassID = c.String(maxLength: 128),
                        StudentID_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserClasses", t => t.ClassID_ClassID)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentID_Id)
                .Index(t => t.ClassID_ClassID)
                .Index(t => t.StudentID_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInClasses", "StudentID_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInClasses", "ClassID_ClassID", "dbo.UserClasses");
            DropForeignKey("dbo.UserClasses", "TeacherName_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserInClasses", new[] { "StudentID_Id" });
            DropIndex("dbo.UserInClasses", new[] { "ClassID_ClassID" });
            DropIndex("dbo.UserClasses", new[] { "TeacherName_Id" });
            DropTable("dbo.UserInClasses");
            DropTable("dbo.UserClasses");
        }
    }
}
