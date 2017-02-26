namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFileId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileRelations", "FileId", c => c.Guid(nullable: false));
            DropColumn("dbo.FileRelations", "FileUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileRelations", "FileUrl", c => c.String(maxLength: 200));
            DropColumn("dbo.FileRelations", "FileId");
        }
    }
}
