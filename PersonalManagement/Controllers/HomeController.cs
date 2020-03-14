using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonalManagement.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("https://www.worldometers.info/coronavirus/");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(res);
                HtmlNode tbl = htmlDoc.GetElementbyId("main_table_countries");
                HtmlNode tbody = tbl.SelectSingleNode("//tbody");
                HtmlNodeCollection trColl = tbody.SelectNodes("tr");
                HtmlNode coronaTbl = HtmlNode.CreateNode("<table></table>");
                coronaTbl.Attributes.Add("class", "table table-bordered");
                HtmlNode coronathead = HtmlNode.CreateNode("<thead><tr><th>No</th><th>Quốc gia</th><th>Số ca mắc</th><th>Phục hồi</th><th>Tử vong</th><th>Tỉ lệ tử vong</th></tr></thead>");
                HtmlNode coronatbody = HtmlNode.CreateNode("<tbody></tbody>");
                coronaTbl.AppendChild(coronathead);
                CultureInfo viCulture = new CultureInfo("vi-VN");
                for (int i = 0; i < 3; i++)
                {
                    HtmlNode tr = trColl[i];
                    HtmlNodeCollection dtd = tr.SelectNodes("td");
                    HtmlNode newTr = HtmlNode.CreateNode("<tr></tr>");
                    HtmlNodeCollection tdColl = new HtmlNodeCollection(newTr);
                    int soCaMac = int.Parse(dtd[1].InnerText.Trim(), NumberStyles.AllowThousands);
                    int phuchoi = int.Parse(dtd[5].InnerText.Trim(), NumberStyles.AllowThousands);
                    int tuvong = int.Parse(dtd[3].InnerText.Trim(), NumberStyles.AllowThousands);
                    double rate = Math.Round(1.0 * tuvong / soCaMac * 100, 2);
                    tdColl.Add(HtmlNode.CreateNode($"<td>{i + 1}</td>"));
                    tdColl.Add(HtmlNode.CreateNode($"<td>{dtd[0].InnerText.Trim()}</td>"));
                    tdColl.Add(HtmlNode.CreateNode($"<td>{soCaMac.ToString("N0", viCulture)}</td>"));
                    tdColl.Add(HtmlNode.CreateNode($"<td>{phuchoi.ToString("N0", viCulture)}</td>"));
                    tdColl.Add(HtmlNode.CreateNode($"<td>{tuvong.ToString("N0", viCulture)}</td>"));
                    tdColl.Add(HtmlNode.CreateNode($"<td>{rate}%</td>"));
                    newTr.AppendChildren(tdColl);
                    coronatbody.AppendChild(newTr);

                }
                coronaTbl.AppendChild(coronatbody);

                res = coronaTbl.OuterHtml;
                ViewBag.WorldCoronaTable = res;
            }
            string content = "_congbothongke_WAR_coronadvcportlet_ma={0}&_congbothongke_WAR_coronadvcportlet_jsonData=%5B%7B%22name%22%3A%22Ha+Noi%22%2C%22ma%22%3A%2201%22%2C%22soCaNhiem%22%3A%223%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22name%22%3A%22aaaaa%22%2C%22ma%22%3A%22%22%2C%22soCaNhiem%22%3A%2220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22120%22%7D%2C%7B%22name%22%3A%22bbb%22%2C%22ma%22%3A%22%22%2C%22soCaNhiem%22%3A%2220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22120%22%7D%2C%7B%22ma%22%3A%2202%22%2C%22soCaNhiem%22%3A%220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%22--Ch%E1%BB%8Dn+%C4%91%E1%BB%8Ba+ph%C6%B0%C6%A1ng--%22%2C%22soCaNhiem%22%3A%22%22%2C%22tuVong%22%3A%22%22%2C%22nghiNhiem%22%3A%22%22%7D%2C%7B%22ma%22%3A%22VNALL%22%2C%22soCaNhiem%22%3A%2238%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22210%22%2C%22binhPhuc%22%3A%2216%22%2C%22cachLy%22%3A%222.336%22%7D%2C%7B%22ma%22%3A%2279%22%2C%22soCaNhiem%22%3A%224+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%223+%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2226%22%2C%22soCaNhiem%22%3A%2211%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%2210%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2238%22%2C%22soCaNhiem%22%3A%221%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%221%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2256%22%2C%22soCaNhiem%22%3A%221%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%221%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2208%22%2C%22soCaNhiem%22%3A%220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2222%22%2C%22soCaNhiem%22%3A%224+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2246%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2210%22%2C%22soCaNhiem%22%3A%222%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2237%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2248%22%2C%22soCaNhiem%22%3A%222+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2249%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2260%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%5D";
            List<string> maTinh = new List<string>() { "01", "48" };
            foreach (string ma in maTinh)
            {
                string temp = string.Format(content, ma);
                HttpContent hc = new StringContent(temp);
                hc.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                response = await client.PostAsync("https://ncov.moh.gov.vn/web/guest/trang-chu?p_p_id=congbothongke_WAR_coronadvcportlet&p_p_lifecycle=2&p_p_resource_id=getItemByMa", hc);
            }

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