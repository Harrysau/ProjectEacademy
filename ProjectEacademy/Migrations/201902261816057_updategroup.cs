namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategroup : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserInClasses");
            AddColumn("dbo.UserInClasses", "classdetail", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserInClasses", "classdetail");
            DropColumn("dbo.UserInClasses", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInClasses", "Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.UserInClasses");
            DropColumn("dbo.UserInClasses", "classdetail");
            AddPrimaryKey("dbo.UserInClasses", "Id");
        }
    }
}
