namespace WebAppProt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        alias = c.String(nullable: false),
                        password = c.String(nullable: false),
                        role = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        validation_token = c.Guid(nullable: false),
                        date_created = c.DateTime(nullable: false),
                        date_updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Participants", "date_created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Participants", "last_name", c => c.String(nullable: false));
            AlterColumn("dbo.Participants", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Participants", "email", c => c.String());
            AlterColumn("dbo.Participants", "last_name", c => c.String());
            DropColumn("dbo.Participants", "date_created");
            DropTable("dbo.Accounts");
        }
    }
}
