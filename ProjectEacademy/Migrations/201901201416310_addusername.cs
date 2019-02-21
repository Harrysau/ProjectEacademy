namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "User", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "User");
        }
    }
}
