namespace PersonalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSkillID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserSkills", "Skill_SkillID", "dbo.Skills");
            RenameColumn(table: "dbo.ApplicationUserSkills", name: "Skill_SkillID", newName: "Skill_ID");
            RenameIndex(table: "dbo.ApplicationUserSkills", name: "IX_Skill_SkillID", newName: "IX_Skill_ID");
            //DropPrimaryKey("dbo.Skills", new[] {"SkillID" });
            //AddColumn("dbo.Skills", "ID", c => c.Int(nullable: false, identity: true));
            RenameColumn(table: "dbo.Skills", name: "SkillID", newName: "ID");

            //AddPrimaryKey("dbo.Skills", "ID");
            AddForeignKey("dbo.ApplicationUserSkills", "Skill_ID", "dbo.Skills", "ID", cascadeDelete: true);
            //DropColumn("dbo.Skills", "SkillID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "SkillID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.ApplicationUserSkills", "Skill_ID", "dbo.Skills");
            DropPrimaryKey("dbo.Skills");
            DropColumn("dbo.Skills", "ID");
            AddPrimaryKey("dbo.Skills", "SkillID");
            RenameIndex(table: "dbo.ApplicationUserSkills", name: "IX_Skill_ID", newName: "IX_Skill_SkillID");
            RenameColumn(table: "dbo.ApplicationUserSkills", name: "Skill_ID", newName: "Skill_SkillID");
            AddForeignKey("dbo.ApplicationUserSkills", "Skill_SkillID", "dbo.Skills", "SkillID", cascadeDelete: true);
        }
    }
}
