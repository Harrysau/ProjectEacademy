namespace ProjectEacademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        TeacherId = c.String(),
                        ClassId = c.String(),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Type = c.String(),
                        Date = c.String(),
                        DeadLine = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileDetails", "PostId", "dbo.Posts");
            DropIndex("dbo.FileDetails", new[] { "PostId" });
            DropTable("dbo.Posts");
            DropTable("dbo.FileDetails");
        }
    }
}
