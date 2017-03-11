namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "FileId", c => c.Guid(nullable: false));
        }
    }
}
