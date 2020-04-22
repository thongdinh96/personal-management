namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkLinkColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "WorkLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "WorkLink");
        }
    }
}
