namespace IndustryExplorersData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201511010625553_Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "Resume", c => c.Binary(nullable: false));
            AddColumn("dbo.Participants", "ResumeName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participants", "ResumeName");
            DropColumn("dbo.Participants", "Resume");
        }
    }
}
