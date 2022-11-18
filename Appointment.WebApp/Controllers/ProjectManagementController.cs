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
using VUPayRoll.WebApp.Models;
using static VUPayRoll.ViewModel.Enumerations;

namespace VUPayRoll.WebApp.Controllers
{
    [Authorize]
    public class ProjectManagementController : Controller
    {
        // GET: ProjectManagement
        public ActionResult ProjectManage()
        {
            var tasktype = Enum.GetValues(typeof(TaskType))
                .Cast<TaskType>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.tasktype = tasktype;

            IEnumerable<ProjectsViewModel> model = null;
            IEnumerable<TaskViewModel> model1 = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("ProjectManagement/getallProjects/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<ProjectsViewModel>>();
                readTask.Wait();
                model = readTask.Result;


                var responseTask1 = client.GetAsync("ProjectManagement/Tasks/");
                responseTask1.Wait();
                var result1 = responseTask1.Result;
                var readTask1 = result1.Content.ReadAsAsync<IEnumerable<TaskViewModel>>();
                readTask1.Wait();
                model1 = readTask1.Result;
            }
            var model2 = new ProjectsTasks
            {
                Projects=model,
                Tasks=model1
            };

            return View(model2);
        }
        public ActionResult EmployeesUnderProject()
        {
            List<EmployeeViewModel> employeeinfoviewModel = null;
            List<ProjectNameViewModel> projectNameViewModel = null;
            using (var client = Utility.GetHttpClient())
            {
                var employeeinfo = Task.Run(async () => await client.GetAsync("Employee/getall")).Result;
                if (employeeinfo.IsSuccessStatusCode)
                {
                    employeeinfoviewModel = Task.Run(async () => await employeeinfo.Content.ReadAsAsync<List<EmployeeViewModel>>()).Result;
                }
                ViewBag.employeeinfo = employeeinfoviewModel;

                var projectNameinfo = Task.Run(async () => await client.GetAsync("ProjectManagement/getall/")).Result;
                if (projectNameinfo.IsSuccessStatusCode)
                {
                    projectNameViewModel = Task.Run(async () => await projectNameinfo.Content.ReadAsAsync<List<ProjectNameViewModel>>()).Result;
                }
                ViewBag.projectName = projectNameViewModel;
            }
            return View();
        }
        public async Task<ActionResult> Create_Projects(int Project, int[] Employees)
        {
            List<ProjectsViewModel> projects = new List<ProjectsViewModel>();
            foreach (var item in Employees)
            {
                var project = new ProjectsViewModel
                {
                    ProjectNameId= Project,
                    EmployeeId=item,
                };
                projects.Add(project);
            }
            foreach (var item in projects)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(item);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync("ProjectManagement/createProjects", byteContent);
                }
            }
            return RedirectToAction("ProjectManage", "ProjectManagement");
        }
        public ActionResult ProjectNameManage()
        {
            return View();
        }
        public JsonResult ProjectNameList()
        {
            IEnumerable<ProjectNameViewModel> model = null;

            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("ProjectManagement/getall/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<ProjectNameViewModel>>();
                readTask.Wait();
                model = readTask.Result;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> AddProjectName(string Name,string StartDate,string Language,string TFSStatusLastUpdated,string TeamLead)
        {
            var model = new ProjectNameViewModel
            {
                Name=Name,
                StartDate=StartDate,
                Language=Language,
                TFSStatusLastUpdated=TFSStatusLastUpdated,
                TeamLead=TeamLead
            };
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("ProjectManagement/Add", byteContent);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteProjectName(int ID)
        {
            bool result1 = false;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.DeleteAsync("ProjectManagement/Delete/" + ID + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    result1 = true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(result1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProject(int PId)
        {
            bool result1 = false;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.DeleteAsync("ProjectManagement/DeleteProject/" + PId + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    result1 = true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(result1,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> EmployeeTask(int PId,int EmpId,int Task,string Description)
        {
            var model = new TaskViewModel
            {
                ProjId=PId,
                EmpId=EmpId,
                TaskType=(TaskType)Task,
                Description=Description
            };
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("ProjectManagement/EmployeeTask", byteContent);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteTask(int Id,int EmpId, int PId)
        {
            bool result1 = false;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("ProjectManagement/DeleteTask/" + Id + "/" + EmpId + "/" + PId + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    result1 = true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(result1, JsonRequestBehavior.AllowGet);
        }
    }
}