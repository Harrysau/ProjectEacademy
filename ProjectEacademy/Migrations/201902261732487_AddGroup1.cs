namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroup1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserClasses", "TeacherName_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInClasses", "ClassID_ClassID", "dbo.UserClasses");
            DropForeignKey("dbo.UserInClasses", "StudentID_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserClasses", new[] { "TeacherName_Id" });
            DropIndex("dbo.UserInClasses", new[] { "ClassID_ClassID" });
            DropIndex("dbo.UserInClasses", new[] { "StudentID_Id" });
            AddColumn("dbo.UserClasses", "TeacherID", c => c.String());
            AddColumn("dbo.UserInClasses", "ClassID", c => c.String());
            AddColumn("dbo.UserInClasses", "StudentID", c => c.String());
            DropColumn("dbo.UserClasses", "TeacherName_Id");
            DropColumn("dbo.UserInClasses", "ClassID_ClassID");
            DropColumn("dbo.UserInClasses", "StudentID_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInClasses", "StudentID_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserInClasses", "ClassID_ClassID", c => c.String(maxLength: 128));
            AddColumn("dbo.UserClasses", "TeacherName_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.UserInClasses", "StudentID");
            DropColumn("dbo.UserInClasses", "ClassID");
            DropColumn("dbo.UserClasses", "TeacherID");
            CreateIndex("dbo.UserInClasses", "StudentID_Id");
            CreateIndex("dbo.UserInClasses", "ClassID_ClassID");
            CreateIndex("dbo.UserClasses", "TeacherName_Id");
            AddForeignKey("dbo.UserInClasses", "StudentID_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserInClasses", "ClassID_ClassID", "dbo.UserClasses", "ClassID");
            AddForeignKey("dbo.UserClasses", "TeacherName_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
