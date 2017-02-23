namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFileRelation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FileRelations", "ActivityInstanceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileRelations", "ActivityInstanceId", c => c.Long());
        }
    }
}
