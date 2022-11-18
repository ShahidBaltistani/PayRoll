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
using VUPayRoll.WebApp;

namespace Appointment.WebApp.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        [HttpGet]
        public ActionResult Create(int EmployeeId)
        {
            ViewBag.EmployeeId = EmployeeId;
            return View();
        }
        [HttpGet]
        public ActionResult Documents(int Id)
        {
            ViewBag.EmpId = Id;

            IEnumerable<DocumentViewModel> documentViewModels = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Document/GetDocumentByEmployeeId/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IEnumerable<DocumentViewModel>>();
                readTask.Wait();
                documentViewModels = readTask.Result;
            }
            return View(documentViewModels);

        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,string Description,int EmployeeInfoId)
        {
            using (var client = Utility.GetHttpClient())
            {
                string filename = file.FileName.ToLower().ToString();
                string[] exts = filename.Split('.');
                string name = exts[0].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                string ext = exts[1].ToString();
                string FinalResult = name + "." + ext;

                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), Path.GetFileName(FinalResult));
                    file.SaveAs(path);
                }
                var documentViewModel = new DocumentViewModel
                {
                    EmployeeId = EmployeeInfoId,
                    Description = Description,
                    File = FinalResult,
                    FileName = file.FileName
                };
                var myContent = JsonConvert.SerializeObject(documentViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = Task.Run(async () => await client.PostAsync("Document/add", byteContent)).Result;
            }
            return RedirectToAction("Employees", "Home");
        }


        //[HttpGet]
        //public FileResult DownLoadFile(int id)
        //{
        //    DocumentViewModel model = new DocumentViewModel();

        //    using (var client = Utility.GetHttpClient())
        //    {
        //        var responseTask = client.GetAsync("Document/GetById/" + id + "");
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<DocumentViewModel>();
        //            readTask.Wait();
        //            model = readTask.Result;

        //            byte[] bytes = Convert.FromBase64String(model.File);

        //            return File(bytes, "application/pdf", model.FileName);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Server error try after some time.");
        //        }
        //    }
        //    return File(model.File, "application/pdf", model.FileName);
        //}

        public ActionResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Document/Delete/" + id + "");
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

        public ActionResult DownloadFileFromFolder(string fileName)
        {
            string fullName = Server.MapPath("~/UploadedFiles/" + fileName);
            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}