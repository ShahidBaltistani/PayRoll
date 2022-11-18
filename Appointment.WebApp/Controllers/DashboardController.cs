using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VUPayRoll.ViewModel;
using VUPayRoll.ViewModel.Account;
using VUPayRoll.WebApp;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Dboard()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Users = Users();
            model.leaves = Leaves();
            model.Attendances = Attendances();
            model.Announcements = Announces();
            model.Employees = Employees();

            return View(model);
        }

        public IEnumerable<SignUpViewModel> Users()
        {
            IEnumerable<SignUpViewModel> users = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Account/GetUsers/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<SignUpViewModel>>();
                readTask.Wait();
                users = readTask.Result;
            }
            return users;
        }
        public IEnumerable<LeaveViewModel> Leaves()
        {
            int UserId= Convert.ToInt32(Session["UserId"].ToString());

            IEnumerable<LeaveViewModel> leaves = null;
            using (var client = Utility.GetHttpClient())
            {
                if (User.Identity.IsAuthenticated && Session["Role"] != null && Session["Role"].ToString() == "1"){
                    var responseTask = client.GetAsync("Leave/GetAllNotApprovedLeaveForAdminDashboard/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<LeaveViewModel>>();
                    readTask.Wait();
                    leaves = readTask.Result;
                }
                else
                {
                    var responseTask = client.GetAsync("Leave/GetAllByUserId/" + UserId + "");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<LeaveViewModel>>();
                    readTask.Wait();
                    leaves = readTask.Result;
                }
            }
            return leaves;
        }
        public IEnumerable<AttendanceViewModel> Attendances()
        {
            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            IEnumerable<AttendanceViewModel> attendances = null;

            using (var client = Utility.GetHttpClient())
            {
                if (User.Identity.IsAuthenticated && Session["Role"] != null && Session["Role"].ToString() == "1")
                {
                    var responseTask = client.GetAsync("attendance/NotApprovedList/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<AttendanceViewModel>>();
                    readTask.Wait();
                    attendances = readTask.Result;
                }
                else
                {
                    var responseTask = client.GetAsync("attendance/GetAllByUserId/" + UserId + "");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<AttendanceViewModel>>();
                    readTask.Wait();
                    attendances = readTask.Result;
                }
            }
            return attendances;

        }

        public IEnumerable<AnnouncementViewModel> Announces()
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
            return announcements;

        }
        public IEnumerable<EmployeeViewModel> Employees()
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
            return employees;
        }

        //public ActionResult Image()
        //{
        //    string Pic = "";

        //    int UserId = Convert.ToInt32(Session["UserId"].ToString());
        //    if (UserId==1)
        //    {
        //        Pic = "/Content/Images/fuser.png";
        //    }
        //    IEnumerable<EmployeeViewModel> employees = null;
        //    using (var client = Utility.GetHttpClient())
        //    {
        //        var responseTask = client.GetAsync("Employee/getall/");
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeViewModel>>();
        //        readTask.Wait();
        //        employees = readTask.Result;
        //    }
        //    foreach (var item in employees)
        //    {
        //        if (item.UserId==UserId)
        //        {
        //            Pic=item.ProfileImage;
        //        }
        //    }
        //    return Json(Pic);
        //}
    }
}