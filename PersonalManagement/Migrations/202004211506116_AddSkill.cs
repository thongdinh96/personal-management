namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkill : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.ApplicationUserSkills",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Skill_SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Skill_SkillID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_SkillID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Skill_SkillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserSkills", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.ApplicationUserSkills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserSkills", new[] { "Skill_SkillID" });
            DropIndex("dbo.ApplicationUserSkills", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserSkills");
            DropTable("dbo.Skills");
        }
    }
}
