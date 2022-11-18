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

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        public ActionResult Export()
        {
            List<Employee> employees = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Employee/getall/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<List<Employee>>();
                readTask.Wait();
                employees = readTask.Result;
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "CrystalReport1.rpt"));
            rd.SetDataSource(employees);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Employee.pdf");
        }
    }
}