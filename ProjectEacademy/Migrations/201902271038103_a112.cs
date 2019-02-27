namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a112 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserInClasses");
            AlterColumn("dbo.UserInClasses", "classdetail", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserInClasses", "classdetail");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserInClasses");
            AlterColumn("dbo.UserInClasses", "classdetail", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserInClasses", "classdetail");
        }
    }
}
