namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPostClassId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ClassId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ClassId");
        }
    }
}
