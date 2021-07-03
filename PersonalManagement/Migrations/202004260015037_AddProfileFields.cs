namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rank", c => c.Byte(nullable: false));
            AddColumn("dbo.AspNetUsers", "ExperEnum", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "HourlyRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "TotalProjects", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "EnglishLevel", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Availability", c => c.String());
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Bio");
            DropColumn("dbo.AspNetUsers", "Availability");
            DropColumn("dbo.AspNetUsers", "EnglishLevel");
            DropColumn("dbo.AspNetUsers", "TotalProjects");
            DropColumn("dbo.AspNetUsers", "HourlyRate");
            DropColumn("dbo.AspNetUsers", "ExperEnum");
            DropColumn("dbo.AspNetUsers", "Rank");
        }
    }
}
