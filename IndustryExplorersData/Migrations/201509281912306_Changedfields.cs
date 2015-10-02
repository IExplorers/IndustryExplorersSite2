namespace WebAppProt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partners", "website", c => c.String());
            DropColumn("dbo.Partners", "web_site");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partners", "web_site", c => c.String());
            DropColumn("dbo.Partners", "website");
        }
    }
}
