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
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Gia đình"
                },
                new Category()
                {
                    Name = "Sự nghiệp"
                },
                new Category()
                {
                    Name = "Bản thân"
                },
                new Category()
                {
                    Name = "Khoa học - kĩ thuật"
                },
                new Category()
                {
                    Name = "Triết học"
                },
                new Category()
                {
                    Name = "Kinh doanh"
                }
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            var goals = new List<Goal>()
            {
                new Goal()
                {
                    Name = "Thành thạo C# .NET",
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2020, 12, 31),
                    CategoryId = categories.Single( s => s.Name == "Sự nghiệp").Id
                },
                new Goal()
                {
                    Name = "Thành thạo SQL Server",
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2020, 12, 31),
                    CategoryId = categories.Single( s => s.Name == "Sự nghiệp").Id
                },
                new Goal()
                {
                    Name = "Thành thạo Javascript",
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2020, 12, 31),
                    CategoryId = categories.Single( s => s.Name == "Sự nghiệp").Id
                }
            };
            goals.ForEach(s => context.Goals.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}
