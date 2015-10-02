namespace WebAppProt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        participant_id = c.Guid(nullable: false),
                        organization = c.String(),
                        first_name = c.String(nullable: false),
                        last_name = c.String(),
                        email = c.String(),
                        activated = c.Boolean(nullable: false),
                        validation_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.participant_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Participants");
        }
    }
}
