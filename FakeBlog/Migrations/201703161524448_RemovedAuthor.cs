namespace FakeBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "Author_Id");
            AddForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.Posts", "Author_Id");
        }
    }
}
