using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalManagement.Models
{
    public class GoalsViewModel
    {
        public string Category { get; set; }
        public List<Goal> Goals { get; set; }
    }
}