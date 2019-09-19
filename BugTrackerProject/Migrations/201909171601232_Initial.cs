namespace BugTrackerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tickets", name: "AssignedUser_Id", newName: "AssignedUserId");
            RenameColumn(table: "dbo.Tickets", name: "Creator_Id", newName: "CreatorId");
            RenameIndex(table: "dbo.Tickets", name: "IX_Creator_Id", newName: "IX_CreatorId");
            RenameIndex(table: "dbo.Tickets", name: "IX_AssignedUser_Id", newName: "IX_AssignedUserId");
            DropColumn("dbo.Tickets", "OwnerUserId");
            DropColumn("dbo.Tickets", "AssignedToUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "AssignedToUserId", c => c.String());
            AddColumn("dbo.Tickets", "OwnerUserId", c => c.String());
            RenameIndex(table: "dbo.Tickets", name: "IX_AssignedUserId", newName: "IX_AssignedUser_Id");
            RenameIndex(table: "dbo.Tickets", name: "IX_CreatorId", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.Tickets", name: "CreatorId", newName: "Creator_Id");
            RenameColumn(table: "dbo.Tickets", name: "AssignedUserId", newName: "AssignedUser_Id");
        }
    }
}
