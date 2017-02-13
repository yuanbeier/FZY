namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateHomePic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePics", "Url", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePics", "Url");
        }
    }
}
