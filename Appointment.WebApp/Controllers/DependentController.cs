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
using static VUPayRoll.ViewModel.Enumerations;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class DependentController : Controller
    {
        [HttpGet]
        public ActionResult Create(int EmployeeId)
        {
            ViewBag.EmployeeId = EmployeeId;

            var gender = Enum.GetValues(typeof(GenderEnum))
                .Cast<GenderEnum>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.Gender = gender;
            var maritalStatus = Enum.GetValues(typeof(MaritalStatusEnum))
                .Cast<MaritalStatusEnum>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.MaritalStatus = maritalStatus;

            List<RelationshipTypeViewModel> relationshipTypeviewModel = null;
            List<NationalityViewModel> nationalityViewModel = null;
            using (var client = Utility.GetHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var relationshipType = Task.Run(async () => await client.GetAsync("Dependent/GetRelationshipTypes")).Result;

                if (relationshipType.IsSuccessStatusCode)
                {
                    relationshipTypeviewModel = Task.Run(async () => await relationshipType.Content.ReadAsAsync<List<RelationshipTypeViewModel>>()).Result;
                }
                ViewBag.relationshipType = relationshipTypeviewModel;

                var nationality = Task.Run(async () => await client.GetAsync("Employee/GetNationalities")).Result;
                if (nationality.IsSuccessStatusCode)
                {
                    nationalityViewModel = Task.Run(async () => await nationality.Content.ReadAsAsync<List<NationalityViewModel>>()).Result;
                }
                ViewBag.nationality = nationalityViewModel;
            }
           
                return View();
        }
        [HttpPost]
        public JsonResult Create(List<DependentViewModel> dependentViewModels)
        {
            foreach (var dependentViewModel in dependentViewModels)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(dependentViewModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = Task.Run(async () => await client.PostAsync("Dependent/add", byteContent)).Result;
                }
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Dependent/DeleteAsync/" + id + "");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Employees", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json("True", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dependents(int Id)
        {
            ViewBag.EmpId = Id;

            IEnumerable<DependentViewModel> dependentViewModels = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Dependent/GetDependentByEmployeeId/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<DependentViewModel>>();
                readTask.Wait();
                dependentViewModels = readTask.Result;
            }
            return View(dependentViewModels);
        }
    }
}