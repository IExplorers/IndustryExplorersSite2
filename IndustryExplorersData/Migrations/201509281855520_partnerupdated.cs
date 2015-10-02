namespace WebAppProt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partnerupdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        organization_name = c.String(nullable: false),
                        contact_name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        web_site = c.String(),
                        date_created = c.DateTime(nullable: false),
                        validation_id = c.Guid(nullable: false),
                        activated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Partners");
        }
    }
}
