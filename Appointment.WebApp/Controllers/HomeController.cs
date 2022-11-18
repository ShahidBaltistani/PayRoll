using Appointment.WebApp.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VUPayRoll.ViewModel;
using VUPayRoll.WebApp;
using Http = System.Web.HttpContext;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Employees()
        {
            int UserId = Convert.ToInt32(Http.Current.Session["UserId"].ToString());
            int RoleId = Convert.ToInt32(Http.Current.Session["Role"].ToString());

            IEnumerable<EmployeeViewModel> employees = null;
            using (var client = Utility.GetHttpClient())
            {
                if (RoleId== 1)
                {
                    var responseTask = client.GetAsync("Employee/getall/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeViewModel>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    var responseTask = client.GetAsync("Employee/EmployeeListById/" + UserId + "");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeViewModel>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
            }
            return View(employees);
        }

        public ActionResult EmployeesSalaryStatus()
        {
            IEnumerable<EmployeeViewModel> employees = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Employee/getall/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeViewModel>>();
                readTask.Wait();
                employees = readTask.Result;
            }
            return View(employees);
        }
    }
}