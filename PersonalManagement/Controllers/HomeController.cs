using Microsoft.AspNet.Identity.Owin;
using PersonalManagement.BL;
using PersonalManagement.Filters;
using PersonalManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
namespace PersonalManagement.Controllers
{
    
    [RequireHttps]
    [HeaderFilter]
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
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
        [Route("GetYoutube")]
        public ActionResult GetYoutube()
        {


            return View();
        }
        [Route("GoalManagement")]
        public ActionResult GoalManagement()
        {
            var model = _ctx.Goals.Include("Category").GroupBy(g => g.Category.Name, g => g, (key, goals) => new GoalsViewModel() { Category = key, Goals = goals.ToList() });
            return View(model);
        }
        //[HttpPost]
        //public ActionResult GoalManagementCreate()
        //{
        //    Goal goal = new Goal() { }
            
        //}
        public ActionResult GetCategories()
        {
            List<object> arr = new List<object>();
            IEnumerable<Category> categories = _ctx.Categories;
            foreach (Category category in categories)
            {
                arr.Add(new { value = category.Id, text = category.Name });
            }
            return Json(arr);
        }
        [Route("FamousTalk")]
        public ActionResult FamousTalk()
        {
            return View();
        }
        [Route("UsefulKnowledge")]
        public ActionResult UsefulKnowledge()
        {
            return View();
        }
        [Route("PsychologicalTips")]
        public ActionResult PsychologicalTips()
        {
            return View();
        }
        public ActionResult AlertCenter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AlertCenter(int? id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                if (id == null)
                {
                    foreach (UserActivity item in ctx.UserActivities)
                    {
                        item.IsRead = true;
                    }
                }
                else
                {
                    UserActivity ua = ctx.UserActivities.Find(id);
                    ua.IsRead = true;

                }
                ctx.SaveChanges();
            }
            return new HttpStatusCodeResult(204);
        }
    }
}