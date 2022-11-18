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

namespace VUPayRoll.WebApp.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        [HttpGet]
        public ActionResult A_Apply()
        {
            var At = Enum.GetValues(typeof(AttendanceType))
                .Cast<AttendanceType>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.At = At;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> A_Apply(AttendanceViewModel attendance)
        {
            attendance.IsApproved = ApprovalStatus.InProcess;
            attendance.UserId = Convert.ToInt32(Session["UserId"].ToString());
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(attendance);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("attendance/Add", byteContent);
            }
            return RedirectToAction("Dboard", "Dashboard");
        }
        public JsonResult GetAttendanceDetail(int Id)
        {
            AttendanceViewModel attendance;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("attendance/GetById/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<AttendanceViewModel>();
                readTask.Wait();
                attendance = readTask.Result;
            }
            return Json(attendance);
        }
        public JsonResult Approved(int id)
        {
            bool result = false;
            using (var client = Utility.GetHttpClient())
            {
                var country = Task.Run(async () => await client.GetAsync("attendance/Approved/" + id + "")).Result;
                if (country.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Rejected(int id)
        {
            bool result = false;
            using (var client = Utility.GetHttpClient())
            {
                var country = Task.Run(async () => await client.GetAsync("attendance/Rejected/" + id + "")).Result;
                if (country.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}