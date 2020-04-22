using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PersonalManagement.BL;
namespace PersonalManagement.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var sim = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (sim != null)
            {
                var userName = User.Identity.Name;
                var usr = await sim.FindByNameAsync(userName);
                Session["EmailConfirmed"] = usr?.EmailConfirmed;
            }
            string worldCoData = await DataService.GetWorldCoronaData();
            string vnCoData = await DataService.GetVietnamCoronaData();
            ViewBag.WorldCoronaTable = worldCoData == "" ? "Không có dữ liệu" : worldCoData;
            ViewBag.VietNamCoronaTable = vnCoData == "" ? "Không có dữ liệu" : vnCoData;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetYoutube()
        {


            return View();
        }
    }
}