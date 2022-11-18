using Appointment.WebApp.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VUPayRoll.ViewModel;
using VUPayRoll.WebApp;
using static VUPayRoll.Entities.PayRoll_Enumerations;
using Http = System.Web.HttpContext;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        [HttpGet]
        public ActionResult Apply()
        {
            var lt = Enum.GetValues(typeof(LeaveType))
                .Cast<LeaveType>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.lt = lt;

            var ls = Enum.GetValues(typeof(LeaveStatus))
                .Cast<LeaveStatus>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.ls = ls;

            var lhf = Enum.GetValues(typeof(LeavehfDay))
                .Cast<LeavehfDay>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.lhf = lhf;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Apply(LeaveViewModel leave)
        {
            leave.IsApproved = ApprovalStatus.InProcess;

            leave.UserId = Convert.ToInt32(Session["UserId"].ToString());
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(leave);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("Leave/Add", byteContent);
            }
            return RedirectToAction("Dboard", "Dashboard");
        }
        public JsonResult Rejected(int id)
        {
            bool result = false;
            using (var client = Utility.GetHttpClient())
            {
                var country = Task.Run(async () => await client.GetAsync("Leave/Rejected/" + id + "")).Result;
                if (country.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Approved(int id)
        {
            bool result = false;
            using (var client = Utility.GetHttpClient())
            {
                var country = Task.Run(async () => await client.GetAsync("Leave/Approved/" + id + "")).Result;
                if (country.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLeaveDetail(int Id)
        {
            LeaveViewModel leave;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Leave/GetById/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<LeaveViewModel>();
                readTask.Wait();
                leave = readTask.Result;
            }
            return Json(leave);
        }
    }
}