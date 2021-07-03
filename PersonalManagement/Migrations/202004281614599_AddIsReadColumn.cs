namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsReadColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserActivities", "IsRead", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserActivities", "IsRead");
        }
    }
}
