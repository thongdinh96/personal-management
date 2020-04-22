namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobTitleCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "JobTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "JobTitle");
        }
    }
}
