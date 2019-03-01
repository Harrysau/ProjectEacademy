namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPostDeadLine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "DeadLine", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DeadLine");
        }
    }
}
