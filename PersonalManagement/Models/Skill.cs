using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalManagement.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public string SkillName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}