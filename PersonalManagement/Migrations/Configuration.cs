namespace PersonalManagement.Migrations
{
    using PersonalManagement.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonalManagement.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonalManagement.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var skills = new List<Skill>()
            {
                new Skill()
                {
                    SkillName="C#"
                },
                new Skill()
                {
                    SkillName=".NET"
                },
                new Skill()
                {
                    SkillName="ASP.NET MVC"
                }
            };
            skills.ForEach(s => context.Skills.AddOrUpdate(p => p.SkillName, s));
            context.SaveChanges();
        }
    }
}
