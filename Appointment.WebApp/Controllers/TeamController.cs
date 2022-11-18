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
    public class TeamController : Controller
    {
        // GET: Team
        [HttpGet]
        public ActionResult Create_Team()
        {
            List<EmployeeViewModel> employees = null;
            List<EmployeeViewModel> teamleads = null;

            using (var client = Utility.GetHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));




                var employee = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;

                if (employee.IsSuccessStatusCode)
                {
                    employees = Task.Run(async () => await employee.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                }
                ViewBag.employees = employees.Where(x => x.DesignationTypeId == 1 | x.DesignationTypeId == 2).ToList();


                var tl = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;

                if (tl.IsSuccessStatusCode)
                {
                    teamleads = Task.Run(async () => await tl.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                }
                ViewBag.Teamleads = teamleads.Where(x => x.DesignationTypeId == 5).ToList();
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create_Team(int TeamLead, int[] Employees)
        {
            List<TeamViewModel> teams = new List<TeamViewModel>();
            foreach (var item in Employees)
            {
                var team = new TeamViewModel
                {
                    ManagerId = TeamLead,
                    EmployeeId=item
                };
                teams.Add(team);
            }
            foreach (var item in teams)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(item);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync("Employee/createTeam", byteContent);
                }
            }
            return RedirectToAction("TeamList", "Team");
        }
        [HttpGet]
        public ActionResult TeamList()
        {
            IEnumerable<TeamViewModel> teams = null;
            EmployeeViewModel employee;
            List<TeamViewModel> teams1 = new List<TeamViewModel>();
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Employee/GetTeams/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<TeamViewModel>>();
                readTask.Wait();
                teams = readTask.Result;
            }
            foreach (var item in teams)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var responseTask = client.GetAsync("Employee/GetById/" + item.ManagerId + "");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();
                    employee = readTask.Result;
                }

                var team = new TeamViewModel
                {
                    Id=item.Id,
                    ManagerId=item.ManagerId,
                    Manager=employee,
                    EmployeeId=item.EmployeeId,
                    Employee=item.Employee
                };

                teams1.Add(team);
            }
            return View(teams1);
        }
    }
}