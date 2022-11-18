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
using VUPayRoll.WebApp;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class EmployeeAllowanceController : Controller
    {
        [HttpGet]
        public ActionResult Create(int EmployeeId)
        {
            ViewBag.EmployeeId = EmployeeId;

            List<AllowanceTypeViewModel> AllowanceviewModel = null;
            List<EmployeeViewModel> employeeinfoviewModel = null;

            using (var client = Utility.GetHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Allowance = Task.Run(async () => await client.GetAsync("EmployeeAllowance/GetAllowancTypes")).Result;

                if (Allowance.IsSuccessStatusCode)
                {
                    AllowanceviewModel = Task.Run(async () => await Allowance.Content.ReadAsAsync<List<AllowanceTypeViewModel>>()).Result;
                }
                ViewBag.Allowance = AllowanceviewModel;


                var employeeinfo = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;

                if (employeeinfo.IsSuccessStatusCode)
                {
                    employeeinfoviewModel = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                }
                ViewBag.employeeinfo = employeeinfoviewModel;
            }

            return View();
        }
        [HttpPost]
        public JsonResult Create(List<EmployeeAllowanceViewModel> employeeAllowanceViewModels)
        {
            foreach (var employeeAllowanceViewModel in employeeAllowanceViewModels)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(employeeAllowanceViewModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = Task.Run(async () => await client.PostAsync("EmployeeAllowance/add", byteContent)).Result;
                }
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("EmployeeAllowance/Delete/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Employees", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json("True",JsonRequestBehavior.AllowGet);
        }
        public ActionResult Allowances(int Id)
        {
            ViewBag.EmpId = Id;

            IEnumerable<EmployeeAllowanceViewModel> employeeAllowanceViewModels = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("EmployeeAllowance/GetEmployeeAllowanceByEmployeeId/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeAllowanceViewModel>>();
                readTask.Wait();
                employeeAllowanceViewModels = readTask.Result;
            }
            return View(employeeAllowanceViewModels);
        }
    }
}