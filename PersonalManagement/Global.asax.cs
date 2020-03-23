using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PersonalManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        async protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var sim=Request.GetOwinContext().Get<ApplicationUserManager>("ASP.NET Identity");
            //var sim=HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>("ASP.NET Identity");
            //var user=await sim.FindByIdAsync(User.Identity.Name);

            //Session["EmailConfirmed"] = user.EmailConfirmed;

        }
        void Application_PreSendRequestHeaders( Object sender, EventArgs e )
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}
