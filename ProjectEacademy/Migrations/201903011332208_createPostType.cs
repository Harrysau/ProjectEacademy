namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPostType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Type");
        }
    }
}
