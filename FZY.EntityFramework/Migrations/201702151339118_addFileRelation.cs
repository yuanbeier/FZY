namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        KeyId = c.Int(nullable: false),
                        FileUrl = c.String(),
                        FileName = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(),
                        FileType = c.Int(),
                        ActivityInstanceId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileRelations");
        }
    }
}
