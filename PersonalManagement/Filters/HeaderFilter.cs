using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalManagement.Models;
namespace PersonalManagement.Filters
{
    public class HeaderFilterAttribute : ActionFilterAttribute
    {
        ApplicationDbContext ctx = new ApplicationDbContext();


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            IEnumerable<AlertViewModel> model;
            model = from ua in ctx.UserActivities
                    select new AlertViewModel() { Id = ua.Id, Date = ua.Date, Message = ua.Messsage, IsRead = ua.IsRead };
            model = model.OrderByDescending(avm => avm.Date);
            filterContext.Controller.ViewBag.AVM = model;
            filterContext.Controller.ViewBag.CountNotRead = model.Count(avm => !avm.IsRead);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}