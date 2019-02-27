namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmorefieldtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfileImg", c => c.String());
            AddColumn("dbo.AspNetUsers", "SchoolName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SchoolName");
            DropColumn("dbo.AspNetUsers", "ProfileImg");
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
