namespace IndustryExplorersData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParticipantChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "ResumeUrl", c => c.String(nullable: false));
            DropColumn("dbo.Participants", "Question3");
            DropColumn("dbo.Participants", "Resume");
            DropColumn("dbo.Participants", "ResumeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "ResumeName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Participants", "Resume", c => c.Binary(nullable: false));
            AddColumn("dbo.Participants", "Question3", c => c.String(nullable: false));
            DropColumn("dbo.Participants", "ResumeUrl");
        }
    }
}
