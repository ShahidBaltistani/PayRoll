using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VUPayRoll.ViewModel;

namespace VUPayRoll.WebApp.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        // GET: Announcement

        public ActionResult Announce_Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Announce_Create(AnnouncementViewModel announcementViewModel)
        {
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(announcementViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("announcement/Add", byteContent);
            }
            return RedirectToAction("Announces");
        }
        public ActionResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("announcement/Delete/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Announces");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json("True");
        }
        public ActionResult Announces()
        {
            IEnumerable<AnnouncementViewModel> announcements = null;

            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("announcement/getall/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<AnnouncementViewModel>>();
                readTask.Wait();
                announcements = readTask.Result;
            }
            return View(announcements);
        }
    }
}