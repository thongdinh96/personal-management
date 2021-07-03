using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalManagement.Models
{
    public class FamousTalk
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
    }
}