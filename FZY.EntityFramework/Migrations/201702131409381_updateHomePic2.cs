namespace FZY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateHomePic2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePics", "Sort", c => c.Int(nullable: false));
            DropColumn("dbo.HomePics", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomePics", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.HomePics", "Sort");
        }
    }
}
