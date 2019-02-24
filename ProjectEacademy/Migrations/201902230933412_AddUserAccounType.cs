namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAccounType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccType");
        }
    }
}
