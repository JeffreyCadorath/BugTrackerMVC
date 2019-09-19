namespace BugTrackerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReDidModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketTypes", "Type", c => c.String());
            DropColumn("dbo.TicketStatus", "Name");
            DropColumn("dbo.TicketTypes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketTypes", "Name", c => c.String());
            AddColumn("dbo.TicketStatus", "Name", c => c.String());
            DropColumn("dbo.TicketTypes", "Type");
        }
    }
}
