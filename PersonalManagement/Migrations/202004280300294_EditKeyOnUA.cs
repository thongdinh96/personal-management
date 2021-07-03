namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditKeyOnUA : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserActivities");
            AddColumn("dbo.UserActivities", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserActivities", "UserId", c => c.String());
            AddPrimaryKey("dbo.UserActivities", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserActivities");
            AlterColumn("dbo.UserActivities", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserActivities", "Id");
            AddPrimaryKey("dbo.UserActivities", "UserId");
        }
    }
}
