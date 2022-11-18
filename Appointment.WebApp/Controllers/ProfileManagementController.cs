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
using VUPayRoll.ViewModel.Account;
using static VUPayRoll.Entities.PayRoll_Enumerations;

namespace VUPayRoll.WebApp.Controllers
{
    [Authorize]
    public class ProfileManagementController : Controller
    {
        // GET: ProfileManagement
        public ActionResult ProfileManage()
        {
            IEnumerable<SignUpViewModel> users = null;
            IEnumerable<EmployeeViewModel> employees = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("account/GetAlls/");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<SignUpViewModel>>();
                readTask.Wait();
                users = readTask.Result;


                var responseTask1 = client.GetAsync("Employee/getall/");
                responseTask1.Wait();
                var result1 = responseTask1.Result;
                var readTask1 = result1.Content.ReadAsAsync<IEnumerable<EmployeeViewModel>>();
                readTask1.Wait();
                employees = readTask1.Result;
            }
            var model = new ProfileManagementViewModel
            {
                Users= users,
                Employees= employees
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult create(int? UserId)
        {
            ViewBag.UserId = UserId;
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

            var shiftEnum = Enum.GetValues(typeof(ShiftEnum))
                .Cast<ShiftEnum>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.Shift = shiftEnum;
            var payType = Enum.GetValues(typeof(PayTypeEnum))
                .Cast<PayTypeEnum>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.Paytype = payType;
            var salaryPaymentMethod = Enum.GetValues(typeof(SalaryPaymentMethodEnum))
                .Cast<SalaryPaymentMethodEnum>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.PaymentMethod = salaryPaymentMethod;
            var department = Enum.GetValues(typeof(Department))
                .Cast<Department>()
                .Select(t => new EnumModel
                {
                    Id = ((int)t),
                    Name = t.ToString()
                });
            ViewBag.department = department;

            List<ReligionViewModel> religionviewModel = null;
            List<CityViewModel> cityviewModel = null;
            List<CountryViewModel> countryviewModel = null;
            List<DesignationTypeViewModel> designationviewModel = null;
            List<NationalityViewModel> nationalityViewModel = null;

            using (var client = Utility.GetHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var religion = Task.Run(async () => await client.GetAsync("Employee/GetReligions")).Result;

                if (religion.IsSuccessStatusCode)
                {
                    religionviewModel = Task.Run(async () => await religion.Content.ReadAsAsync<List<ReligionViewModel>>()).Result;
                }
                ViewBag.religion = religionviewModel;
                var city = Task.Run(async () => await client.GetAsync("Employee/getCities")).Result;
                if (city.IsSuccessStatusCode)
                {
                    cityviewModel = Task.Run(async () => await city.Content.ReadAsAsync<List<CityViewModel>>()).Result;
                }
                ViewBag.city = cityviewModel;
                var country = Task.Run(async () => await client.GetAsync("Employee/GetCountries")).Result;
                if (country.IsSuccessStatusCode)
                {
                    countryviewModel = Task.Run(async () => await country.Content.ReadAsAsync<List<CountryViewModel>>()).Result;
                }
                ViewBag.country = countryviewModel;
                var designation = Task.Run(async () => await client.GetAsync("Employee/GetDesignationType")).Result;
                if (designation.IsSuccessStatusCode)
                {
                    designationviewModel = Task.Run(async () => await designation.Content.ReadAsAsync<List<DesignationTypeViewModel>>()).Result;
                }
                ViewBag.designation = designationviewModel;
                var nationality = Task.Run(async () => await client.GetAsync("Employee/GetNationalities")).Result;
                if (designation.IsSuccessStatusCode)
                {
                    nationalityViewModel = Task.Run(async () => await nationality.Content.ReadAsAsync<List<NationalityViewModel>>()).Result;
                }
                ViewBag.nationality = nationalityViewModel;
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> create(EmployeeViewModel employeeInfoViewModel, HttpPostedFileBase ProfileImage)
        {

            string filename = ProfileImage.FileName.ToLower().ToString();
            string[] exts = filename.Split('.');
            string name = exts[0].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
            string ext = exts[1].ToString();
            string FinalResult = name + "." + ext;

            if (ProfileImage != null)
            {
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(FinalResult));
                ProfileImage.SaveAs(path);
            }
            employeeInfoViewModel.ProfileImage = FinalResult;

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(employeeInfoViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("Employee/Add", byteContent);
            }
            return RedirectToAction("Employees", "Home");
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(SignUpViewModel signUp)
        {
            try
            {
                if (signUp != null)
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        signUp.RoleId = 2;
                        var myContent = JsonConvert.SerializeObject(signUp);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("account/signup", byteContent);

                        var jsonstring = await senddata.Content.ReadAsStringAsync();

                        if (!senddata.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return RedirectToAction("ProfileManage");
                        }
                    }
                }
                return View();

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}