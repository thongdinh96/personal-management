using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace PersonalManagement.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(string.Format(ConfigurationManager.AppSettings.Get("OWMApiUrl"), "Ho Chi Minh", Keys.OpenWeatherApiKey));
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(res);
                var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");
                res = htmlBody.InnerHtml;
                ViewBag.WeatherHtmlStr = res;
            }
            response= await client.GetAsync("https://www.worldometers.info/coronavirus/");
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
    }
}