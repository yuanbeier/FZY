namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFileRelation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileRelations", "FileUrl", c => c.String(maxLength: 200));
            AlterColumn("dbo.FileRelations", "FileName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileRelations", "FileName", c => c.String());
            AlterColumn("dbo.FileRelations", "FileUrl", c => c.String());
        }
    }
}
