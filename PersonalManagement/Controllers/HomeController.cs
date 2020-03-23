using HtmlAgilityPack;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalManagement.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();
        public async Task<ActionResult> Index()
        {
            var sim=HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if ( sim!=null )
            {
                var userName=User.Identity.Name;
                var usr=await sim.FindByNameAsync(userName);
                Session["EmailConfirmed"] = usr?.EmailConfirmed; 
            }
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.worldometers.info/coronavirus/");
                HtmlNode coronaTbl = null;
                HtmlNode coronathead = null;
                HtmlNode coronatbody = null;
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(res);
                    HtmlNode tbl = htmlDoc.GetElementbyId("main_table_countries");
                    HtmlNode tbody = tbl.SelectSingleNode("//tbody");
                    HtmlNodeCollection trColl = tbody.SelectNodes("tr");
                    coronaTbl = HtmlNode.CreateNode("<table></table>");
                    coronaTbl.Attributes.Add("class", "table table-bordered");
                    coronathead = HtmlNode.CreateNode("<thead><tr><th>No</th><th>Quốc gia</th><th>Số ca mắc</th><th>Phục hồi</th><th>Tử vong</th><th>Tỉ lệ tử vong</th></tr></thead>");
                    coronatbody = HtmlNode.CreateNode("<tbody></tbody>");
                    coronaTbl.AppendChild(coronathead);
                    for (int i = 0; i < 3; i++)
                    {
                        HtmlNode tr = trColl[i];
                        coronatbody.AppendChild(CreateNewTr(tr.SelectNodes("td"), i + 1, false));

                    }

                    for (int i = 3; i < trColl.Count; i++)
                    {
                        HtmlNode fn = trColl[i].SelectSingleNode("td");
                        if (fn.InnerText.Contains("Vietnam"))
                        {
                            coronatbody.AppendChild(CreateNewTr(trColl[i].SelectNodes("td"), i + 1, true));
                            break;
                        }
                    }
                    coronaTbl.AppendChild(coronatbody);

                    res = coronaTbl.OuterHtml;
                    ViewBag.WorldCoronaTable = res;
                }
                string content = "_congbothongke_WAR_coronadvcportlet_ma={0}&_congbothongke_WAR_coronadvcportlet_jsonData=%5B%7B%22name%22%3A%22Ha+Noi%22%2C%22ma%22%3A%2201%22%2C%22soCaNhiem%22%3A%223%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22name%22%3A%22aaaaa%22%2C%22ma%22%3A%22%22%2C%22soCaNhiem%22%3A%2220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22120%22%7D%2C%7B%22name%22%3A%22bbb%22%2C%22ma%22%3A%22%22%2C%22soCaNhiem%22%3A%2220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22120%22%7D%2C%7B%22ma%22%3A%2202%22%2C%22soCaNhiem%22%3A%220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%22--Ch%E1%BB%8Dn+%C4%91%E1%BB%8Ba+ph%C6%B0%C6%A1ng--%22%2C%22soCaNhiem%22%3A%22%22%2C%22tuVong%22%3A%22%22%2C%22nghiNhiem%22%3A%22%22%7D%2C%7B%22ma%22%3A%22VNALL%22%2C%22soCaNhiem%22%3A%2238%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%22210%22%2C%22binhPhuc%22%3A%2216%22%2C%22cachLy%22%3A%222.336%22%7D%2C%7B%22ma%22%3A%2279%22%2C%22soCaNhiem%22%3A%224+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%223+%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2226%22%2C%22soCaNhiem%22%3A%2211%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%2210%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2238%22%2C%22soCaNhiem%22%3A%221%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%221%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2256%22%2C%22soCaNhiem%22%3A%221%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%221%22%2C%22cachLy%22%3A%22%22%7D%2C%7B%22ma%22%3A%2208%22%2C%22soCaNhiem%22%3A%220%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2222%22%2C%22soCaNhiem%22%3A%224+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2246%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2210%22%2C%22soCaNhiem%22%3A%222%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2237%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2248%22%2C%22soCaNhiem%22%3A%222+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2249%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%2C%7B%22ma%22%3A%2260%22%2C%22soCaNhiem%22%3A%221+%22%2C%22tuVong%22%3A%220%22%2C%22nghiNhiem%22%3A%220%22%2C%22binhPhuc%22%3A%220%22%2C%22cachLy%22%3A%220%22%7D%5D";
                coronaTbl = HtmlNode.CreateNode("<table></table>");
                coronaTbl.Attributes.Add("class", "table table-bordered");
                coronathead = HtmlNode.CreateNode("<thead><tr><th>No</th><th>Tỉnh/Thành phố</th><th>Số ca mắc</th><th>Phục hồi</th><th>Tử vong</th><th>Tỉ lệ tử vong</th></tr></thead>");
                coronaTbl.AppendChild(coronathead);
                coronatbody = HtmlNode.CreateNode("<tbody></tbody>");


                Dictionary<string, string> maTinh = await GetDicMaTinh();
                int count = 1;
                List<HtmlNode> vnRow = new List<HtmlNode>();
                foreach (string ma in maTinh.Keys)
                {
                    string temp = string.Format(content, ma);
                    HttpContent hc = new StringContent(temp);
                    hc.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    response = await client.PostAsync("https://ncov.moh.gov.vn/web/guest/trang-chu?p_p_id=congbothongke_WAR_coronadvcportlet&p_p_lifecycle=2&p_p_resource_id=getItemByMa", hc);
                    string jsonRes = response.Content.ReadAsStringAsync().Result;
                    if (jsonRes == "null")
                    {
                        continue;
                    }
                    JObject resjobj = JObject.Parse(jsonRes);
                    string soCaNhiem = resjobj["soCaNhiem"].ToString();
                    if (soCaNhiem == "0") continue;
                    vnRow.Add(CreateNewVNTr(resjobj, maTinh[ma]));
                }
                vnRow.Sort(delegate (HtmlNode r1, HtmlNode r2)
                {
                    int soCaMacR1 = int.Parse(r1.SelectNodes("td")[2].InnerText);
                    int soCaMacR2 = int.Parse(r2.SelectNodes("td")[2].InnerText);
                    if (soCaMacR1 < soCaMacR2)
                    {
                        return 1;
                    }
                    else if (soCaMacR1 == soCaMacR2)
                    {
                        return 0;
                    }
                    return -1;
                });
                foreach (HtmlNode row in vnRow)
                {
                    row.SelectNodes("td")[0].InnerHtml = count + "";
                    coronatbody.AppendChild(row);
                    count++;

                }

                coronaTbl.AppendChild(coronatbody);

                ViewBag.VietNamCoronaTable = coronaTbl.OuterHtml;
            }
            catch (Exception ex)
            {
                ViewBag.VietNamCoronaTable = "Không có dữ liệu";
                ViewBag.WorldCoronaTable = "Không có dữ liệu";

            }
            return View();
        }

        private async Task<Dictionary<string, string>> GetDicMaTinh()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();            
            HttpResponseMessage response = await client.GetAsync("https://ncov.moh.gov.vn/");
            if (response.IsSuccessStatusCode)
            {
                byte[] bytecontentArr = await response.Content.ReadAsByteArrayAsync();
                string htmlpage =  Unzip(bytecontentArr );
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlpage);
                HtmlNode maTinhSelect= htmlDoc.GetElementbyId("_congbothongke_WAR_coronadvcportlet_vietNam");
                HtmlNodeCollection optionColl= maTinhSelect.SelectNodes("option");
                foreach (HtmlNode option in optionColl)
                {
                    if (option.Attributes["value"].Value!="VNALL")
                    {
                        res.Add(option.Attributes["value"].Value, option.InnerText.Trim()); 
                    }
                }
            }
            return res;
        }
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        private HtmlNode CreateNewTr(HtmlNodeCollection dtd, int no,bool highlightFlg)
        {
            CultureInfo viCulture = new CultureInfo("vi-VN");
            HtmlNode newTr = HtmlNode.CreateNode("<tr></tr>");
            if(highlightFlg)
            newTr.Attributes.Add("style", "background-color:yellow");
            HtmlNodeCollection tdColl = new HtmlNodeCollection(newTr);
            int soCaMac = int.Parse(dtd[1].InnerText.Trim(), NumberStyles.AllowThousands);
            int phuchoi = int.Parse(dtd[5].InnerText.Trim(), NumberStyles.AllowThousands);
            int tuvong = dtd[3].InnerText.Trim()==string.Empty? 0:int.Parse(dtd[3].InnerText.Trim(), NumberStyles.AllowThousands);
            double rate = Math.Round(1.0 * tuvong / soCaMac * 100, 2);
            tdColl.Add(HtmlNode.CreateNode($"<td>{no}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{dtd[0].InnerText.Trim()}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{soCaMac.ToString("N0", viCulture)}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{phuchoi.ToString("N0", viCulture)}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{tuvong.ToString("N0", viCulture)}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{rate}%</td>"));
            newTr.AppendChildren(tdColl);
            return newTr;
        }

        private HtmlNode CreateNewVNTr(JObject jsonrow, string tinh)
        {
            CultureInfo viCulture = new CultureInfo("vi-VN");
            HtmlNode newTr = HtmlNode.CreateNode("<tr></tr>");
            HtmlNodeCollection tdColl = new HtmlNodeCollection(newTr);
            int soCaMac = int.Parse(jsonrow["soCaNhiem"]+"");
            int phuchoi = int.Parse(jsonrow["binhPhuc"] + "");
            int tuvong = int.Parse(jsonrow["tuVong"] + "");
            double rate = Math.Round(1.0 * tuvong / soCaMac * 100, 2);
            tdColl.Add(HtmlNode.CreateNode("<td></td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{tinh}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{soCaMac.ToString()}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{phuchoi.ToString()}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{tuvong.ToString()}</td>"));
            tdColl.Add(HtmlNode.CreateNode($"<td>{rate}%</td>"));
            newTr.AppendChildren(tdColl);
            return newTr;
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