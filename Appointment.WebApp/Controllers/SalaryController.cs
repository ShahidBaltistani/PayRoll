using Appointment.WebApp.Models;
using Appointment.WebApp.SalaryReport;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VUPayRoll.ViewModel;
using VUPayRoll.WebApp;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class SalaryController : Controller
    {
        [HttpPost]
        public ActionResult OverAll(string FromDate, string ToDate, int? EmployeeInfoId)
        {
            int Id = EmployeeInfoId.GetValueOrDefault();
            if (EmployeeInfoId == null)
            {
                bool ForChecking = false;
                IEnumerable<SalaryViewModel> salary;
                using (var client = Utility.GetHttpClient())
                {
                    var responseTask = client.GetAsync("Salary/getAll/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<SalaryViewModel>>();
                    readTask.Wait();
                    salary = readTask.Result;
                }
                foreach (var item in salary)
                {
                    if (Convert.ToDateTime(item.FromDate).Date <= Convert.ToDateTime(FromDate).Date && Convert.ToDateTime(item.ToDate).Date >= Convert.ToDateTime(ToDate).Date)
                    {

                        ForChecking = true;
                    }
                }
                if (ForChecking == true)
                {
                    return RedirectToAction("Sorry");
                }
                else
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        var Clear = Task.Run(async () => await client.GetAsync("Salary/Deleted")).Result;
                    }

                    List<EmployeeViewModel> employeeinfoviewModel = null;
                    using (var client = Utility.GetHttpClient())
                    {
                        var employeeinfo = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;
                        if (employeeinfo.IsSuccessStatusCode)
                        {
                            employeeinfoviewModel = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                        }
                    }
                    //Days and Salary Calculation
                    DateTime EndDate = Convert.ToDateTime(ToDate).Date;
                    DateTime StartDate = Convert.ToDateTime(FromDate).Date;
                    string Days = (EndDate - StartDate).TotalDays.ToString();
                    List<SalaryViewModel> salaryS = new List<SalaryViewModel>();


                    IList<EmployeeAllowanceViewModel> employeeAllowanceViewModels = null;
                    foreach (var item in employeeinfoviewModel)
                    {

                        //For Allowance
                        using (var client = Utility.GetHttpClient())
                        {
                            var employeeinfo = Task.Run(async () => await client.GetAsync("EmployeeAllowance/GetEmployeeAllowanceByEmployeeId/" + item.Id + "")).Result;
                            if (employeeinfo.IsSuccessStatusCode)
                            {
                                employeeAllowanceViewModels = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeAllowanceViewModel>>()).Result;
                            }
                        }
                        string Result = "";
                        foreach (var item1 in employeeAllowanceViewModels)
                        {
                            Result = item1.Allowance.Name + "--" + item1.Amount;
                        }

                        string OneDaySalary = (int.Parse(item.BasicSalary) / 30).ToString();
                        string TotalSalary = (int.Parse(OneDaySalary) * int.Parse(Days)).ToString();
                        salaryS.Add(new SalaryViewModel
                        {
                            Allowance = Result,
                            EmployeeId = item.Id,
                            FromDate = FromDate,
                            ToDate = ToDate,
                            Days = Days,
                            BasicSalary = item.BasicSalary,
                            TotalSalary = TotalSalary,
                        });
                    };

                    foreach (var item in salaryS)
                    {
                        using (var client = Utility.GetHttpClient())
                        {
                            var myContent = JsonConvert.SerializeObject(item);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            HttpResponseMessage response = Task.Run(async () => await client.PostAsync("Salary/Add", byteContent)).Result;
                        }
                    }
                    return RedirectToAction("SalaryList");
                }
            }
            else
            {
                bool ForChecking = false;
                IEnumerable<SalaryViewModel> salary;
                using (var client = Utility.GetHttpClient())
                {
                    var responseTask = client.GetAsync("Salary/getAll/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<IEnumerable<SalaryViewModel>>();
                    readTask.Wait();
                    salary = readTask.Result;
                }
                foreach (var item in salary)
                {
                    if (Convert.ToDateTime(item.FromDate).Date <= Convert.ToDateTime(FromDate).Date && Convert.ToDateTime(item.ToDate).Date >= Convert.ToDateTime(ToDate).Date && item.EmployeeId==EmployeeInfoId.GetValueOrDefault())
                    {
                        ForChecking = true;
                    }

                }
                if (ForChecking == true)
                {
                    return RedirectToAction("Sorry");
                }
                using (var client = Utility.GetHttpClient())
                {
                    var Clear = Task.Run(async () => await client.GetAsync("Salary/Deleted")).Result;
                }
                EmployeeViewModel employeeinfoviewModel = null;
                using (var client = Utility.GetHttpClient())
                {
                    var employeeinfo = Task.Run(async () => await client.GetAsync("Employee/GetById/" + Id + "")).Result;
                    if (employeeinfo.IsSuccessStatusCode)
                    {
                        employeeinfoviewModel = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<EmployeeViewModel>()).Result;
                    }
                }
                //Days and Salary Calculation
                DateTime EndDate = Convert.ToDateTime(ToDate).Date;
                DateTime StartDate = Convert.ToDateTime(FromDate).Date;
                string Days = (EndDate - StartDate).TotalDays.ToString();


                IList<EmployeeAllowanceViewModel> employeeAllowanceViewModels = null;
                //For Allowance
                using (var client = Utility.GetHttpClient())
                {
                    var employeeinfo = Task.Run(async () => await client.GetAsync("EmployeeAllowance/GetEmployeeAllowanceByEmployeeId/" + Id + "")).Result;
                    if (employeeinfo.IsSuccessStatusCode)
                    {
                        employeeAllowanceViewModels = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeAllowanceViewModel>>()).Result;
                    }
                }
                string Result = "";
                foreach (var item1 in employeeAllowanceViewModels)
                {
                    Result = item1.Allowance.Name + "--" + item1.Amount;
                }

                string OneDaySalary = (int.Parse(employeeinfoviewModel.BasicSalary) / 30).ToString();
                string TotalSalary = (int.Parse(OneDaySalary) * int.Parse(Days)).ToString();

                var salaryS = new SalaryViewModel
                {
                    Allowance = Result,
                    EmployeeId = EmployeeInfoId.GetValueOrDefault(),
                    FromDate = FromDate,
                    ToDate = ToDate,
                    Days = Days,
                    BasicSalary = employeeinfoviewModel.BasicSalary,
                    TotalSalary = TotalSalary,
                };

                using (var client = Utility.GetHttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(salaryS);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                    var responseTask1 = client.PostAsync("Salary/Add", byteContent);
                    responseTask1.Wait();
                    var result1 = responseTask1.Result;
                    //If success received   
                    if (result1.IsSuccessStatusCode)
                    {
                        return RedirectToAction("SalaryDetail", "Salary", new { Id = EmployeeInfoId });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                
            }
            return RedirectToAction("Index1");
        }
        public ActionResult Index1()
        {
            List<EmployeeViewModel> employeeinfoviewModel = null;
            using (var client = Utility.GetHttpClient())
            {
                var employeeinfo = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;
                if (employeeinfo.IsSuccessStatusCode)
                {
                    employeeinfoviewModel = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                }
                ViewBag.employeeinfo = employeeinfoviewModel;
            }
            return View();
        }
        [HttpGet]
        public ActionResult SalaryDetail(int Id)
        {
            SalaryViewModel salary;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Salary/getById/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<SalaryViewModel>();
                readTask.Wait();
                salary = readTask.Result;
            }
            return View(salary);
        }
        public ActionResult SalarySlip(int Id)
        {
            EmployeeViewModel employee = null;
            SalaryViewModel salaryS = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Salary/getById/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<SalaryViewModel>();
                readTask.Wait();
                salaryS = readTask.Result;


                var responseTask1 = client.GetAsync("Employee/GetById/" + Id + "");
                responseTask1.Wait();
                var result1 = responseTask1.Result;
                var readTask1 = result1.Content.ReadAsAsync<EmployeeViewModel>();
                readTask1.Wait();
                employee = readTask1.Result;
            }

            var salary = new SalarySlip
            {
                Allowance=salaryS.Allowance,
                FromDate=salaryS.FromDate,
                ToDate=salaryS.ToDate,
                Days=salaryS.Days,
                Salary=salaryS.TotalSalary,


                //Temporary Used For Last Name
                BasicSalary=employee.LastName,

                Name=employee.FirstName,
                PhoneNo=employee.Mobile,
                Email=employee.AlternateEmail,
                Address=employee.StreetAddress,
                Designation=employee.DesignationTypes.Name                
            };

            
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/SalaryReport"), "CrystalReport2.rpt"));
            rd.SetDataSource(new[] { salary });
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SalarySlip.pdf");
        }
        public async Task<ActionResult> SendEmail(string Email)
        {
            var model = new EmailFormModel
            {
                ToEmail = Email
            };
            try
            {
                if (ModelState.IsValid)
                {
                    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(model.ToEmail));
                    message.From = new MailAddress("kashifdotnetdeveloper@gmail.com"); message.Subject = "Your email subject";
                    message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = true;
                        var credential = new NetworkCredential
                        {
                            UserName = "kashifdotnetdeveloper@gmail.com",
                            Password = ""
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                        return Json("Email sent!!", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("Email sent!!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult SalaryList()
        {
            IEnumerable<SalaryViewModel> salary;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Salary/getAll/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<SalaryViewModel>>();
                readTask.Wait();
                salary = readTask.Result;
            }
            return View(salary);
        }
        public ActionResult Sorry() => View();
    }
}