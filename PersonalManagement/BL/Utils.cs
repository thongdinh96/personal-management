using PersonalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalManagement.BL
{
    public class Utils
    {
        public static void AddUserActitivy(string userId, string msg)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.UserActivities.Add(new UserActivity()
                {
                    UserId = userId,
                    Date = DateTime.Now,
                    Messsage = msg
                });
                ctx.SaveChanges();
            }
        }
    }
}