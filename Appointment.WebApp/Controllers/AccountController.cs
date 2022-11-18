using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using VUPayRoll.ViewModel.Account;
using VUPayRoll.WebApp;
using Http = System.Web.HttpContext;
using System.Net;
using System.Net.Mime;

namespace Appointment.WebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult ComingSoon()
        {
            return View();
        }
        public JsonResult Approved(int id)
        {
            bool result = false;
            using (var client = Utility.GetHttpClient())
            {
                var country = Task.Run(async () => await client.GetAsync("account/Approved/" + id + "")).Result;
                if (country.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(SignUpViewModel signUp)
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
                            return View("SignIn");
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
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignIn(LoginViewModel login)
        {
            if (login != null)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var Client = new HttpClient();
                    var myContent = JsonConvert.SerializeObject(login);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync("/account/login", byteContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, responseText = "Please Enter Valid Username or Password." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var jsonstring = response.Content.ReadAsStringAsync();
                        dynamic details = JObject.Parse(jsonstring.Result);
                        var loginUser = details["userRepo"];


                        bool status = (bool)loginUser["isApproved"];
                        if (status==true)
                        {
                            var token = details["usertoken"];
                            FormsAuthentication.SetAuthCookie(login.Username, false);
                            Http.Current.Session["UserToken"] = (string)details["usertoken"];
                            Http.Current.Session["Role"] = (string)loginUser["roleId"];
                            Http.Current.Session["UserModel"] = login.Username;
                            Http.Current.Session["UserId"] = (string)loginUser["id"];

                            return RedirectToAction("Dboard", "Dashboard");
                        }
                        else
                        {
                            return View("ComingSoon");
                        }
                    }
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();


            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            Session["UserToken"] = null;
            Session["Role"] = null;
            Session["UserModel"] = null;
            Session["UserId"] = null;

            return RedirectToAction("SignIn", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> UserNameExsist(string userName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        var myContent = JsonConvert.SerializeObject(userName);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("account/Userexist", byteContent);
                        var jsonstring = await senddata.Content.ReadAsStringAsync();
                        if (!senddata.IsSuccessStatusCode)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(jsonstring, MediaTypeNames.Text.Plain);
                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            return Json(jsonstring, MediaTypeNames.Text.Plain);
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
        [HttpGet]
        public ActionResult ExtendSession()
        {
            FormsAuthentication.SetAuthCookie((Http.Current.Session["UserId"]).ToString(), false);
            var data = new { IsSuccess = true };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}