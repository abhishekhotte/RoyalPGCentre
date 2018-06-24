using DAL;
using DAL.Entities;
using DAL.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AboutusController : Controller
    {
        StaffDetailsDAL staffDALObj = new StaffDetailsDAL();
        // GET: Aboutus
        public ActionResult Index()
        {
            return View();
        }


        #region Staff details
        [ActionName("OurStaff")]
        public ActionResult Staff()
        {
            return View();
        }
        
        public string GetStaffListHtml()
        {
            var list = staffDALObj.GetStaffList();
            string profileHtml = "";
            for (int i = 0; i < list.Count; i++)
                profileHtml += "<div id='staff" + list[i].Id + "' class='col-lg-3 col-md-4 col-sm-6 col-xs-12 wow zoomIn fadeInUp filter-teaching-staff'><div class='panel panel-default'><div class='panel-body profile'>" + (User.Identity.IsAuthenticated ? "<i class='fa fa-edit staff-edit-icon' data-staff-id='" + list[i].Id + "' title='Edit'></i>" : "") + "<div class='profile-image'><img id='liSImg" + list[i].Id + "' src='/images/staff/" + list[i].Id + ".jpg' alt='' title='"+list[i].Name+"'></div></div><div class='panel-body'><div class='contact-info'><div class='profile-data-name' id='liSName" + list[i].Id + "'>" + list[i].Name + "</div><div class='profile-data-title' id='liSDesignation" + list[i].Id + "'>" + list[i].Designation + "</div><div class='profile-data-title'><span class='fa-book fa fa-lg profile-icon'></span><span id='liSSubject" + list[i].Id + "'>" + list[i].Subject + "</span></div><div class='profile-data-title'><span class='fa-phone-square fa fa-lg profile-icon'></span><span id='liSMobile" + list[i].Id + "'>" + list[i].Mobile + "</span></div><div class='profile-data-title'><span class='fa-envelope-o fa fa-lg profile-icon'></span><span id='liSEmail" + list[i].Id + "'>" + list[i].Email + "</span></div></div></div></div></div>";
            return profileHtml;

        }

        public ActionResult GetAllStaffDetails()
        {
            try
            {
                return Json(new { result = "success", list = GetStaffListHtml(), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {

                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize]
        public ActionResult AddUpdateStaff(StaffDetails s, string image)
        {
            try
            {
                int staffId = 0;
                var data = staffDALObj.AddUpdate(s, ref staffId);
                JsonSerializer j = new JsonSerializer();
                string newImage = Server.MapPath("~/images/staff/" + staffId + ".jpg");
                if (System.IO.File.Exists(newImage))
                    System.IO.File.Delete(newImage);

                if (image.Contains("data:image/png;base64") || image.Contains("data:image/jpg;base64") || image.Contains("data:image/jpeg;base64"))
                {

                    string x = string.Empty;
                    if (image.Contains("data:image/png;base64,"))
                        x = image.Replace("data:image/png;base64,", "");
                    else if (image.Contains("data:image/jpg;base64,"))
                        x = image.Replace("data:image/jpg;base64,", "");
                    else
                        x = image.Replace("data:image/jpeg;base64,", "");

                    // Convert Base64 String to byte[]
                    byte[] imageBytes = Convert.FromBase64String(x);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true);
                    img.Save(Server.MapPath("~/images/staff/" + staffId + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    System.IO.File.Copy(Server.MapPath("~/images/no-image.jpg"), Server.MapPath("~/images/staff/" + staffId + ".jpg"));
                }
                return Json(new { result = "success", staffId = staffId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }

        } 
        #endregion

        #region Gallery section
        [CustomAuthorize]
        [HttpPost]
        public ActionResult UploadCollegePhotos()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                string path = Server.MapPath("~/images/college");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(path + "\\" + InputFileName);

                    //Save file to server folder  
                    int count = 1;
                    checkIfFileExists:
                    if (System.IO.File.Exists(ServerSavePath))
                    {
                        ServerSavePath = Path.Combine(path + "\\" + InputFileName.Substring(0, InputFileName.LastIndexOf('.')) + count++ + InputFileName.Substring(InputFileName.IndexOf('.')));
                        goto checkIfFileExists;
                    }
                    else
                    {
                        file.SaveAs(ServerSavePath);
                    }

                }
                return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json("error", JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult GetCollegePhotos()
        {
            try
            {
                string tmpImages = "";
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/images/college"));
                FileSystemInfo[] files = di.GetFileSystemInfos();
                files.OrderBy(x => x.LastWriteTime).ToList();
                foreach (var item in files)
                {
                    var img = new FileInfo(item.FullName);
                    tmpImages += "<div class='col-lg-3 col-md-6 col-md-4 wow zoomIn fadeInUp' data-animation-delay='0s' style='animation-delay: 0s;'> <a class='lightbox' href='" + Url.Content(String.Format("~/images/college/{0}", img.Name)) + "'><img src = '" + Url.Content(String.Format("~/images/college/{0}", img.Name)) + "' /></a></div>";
                }
                return Json(new { result = "success", images = tmpImages }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Events
        public ActionResult Events()
        {
            return View();
        }

        public ActionResult GetEventList()

        {
            try
            {
                EventsDAL obj = new EventsDAL();
                return Json(new { result = "success", list = obj.GetEventsList(), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }

        }

        public string GetEventPhotos(int eventId)
        {
            try
            {
                string tmpImages = "";
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/images/events/"+eventId));
                FileSystemInfo[] files = di.GetFileSystemInfos();
                files.OrderBy(x => x.LastWriteTime).ToList();
                foreach (var item in files)
                {
                    var img = new FileInfo(item.FullName);
                    tmpImages += "<div class='col-lg-3 col-md-6 col-md-4 wow zoomIn fadeInUp' data-animation-delay='0s' style='animation-delay: 0s;'> <a class='lightbox' href='" + Url.Content(String.Format("~/images/events/"+eventId+"/{0}", img.Name)) + "'><img src = '" + Url.Content(String.Format("~/images/events/"+eventId+"/{0}", img.Name)) + "' /></a></div>";
                }
                return tmpImages;
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return "error";
            }
        }

        public ActionResult AddEvent(Event e)
        {
            try
            {
                EventsDAL obj = new EventsDAL();
                int eventId = 0;
                    obj.Add(e,ref eventId);
                HttpFileCollectionBase files = Request.Files;
                string path = Server.MapPath("~/images/events/"+ eventId);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(path + "\\" + InputFileName);

                    //Save file to server folder  
                    int count = 1;
                    checkIfFileExists:
                    if (System.IO.File.Exists(ServerSavePath))
                    {
                        ServerSavePath = Path.Combine(path + "\\" + InputFileName.Substring(0, InputFileName.LastIndexOf('.')) + count++ + InputFileName.Substring(InputFileName.IndexOf('.')));
                        goto checkIfFileExists;
                    }
                    else
                    {
                        file.SaveAs(ServerSavePath);
                    }
                }
                return Json(new { result = "success", images = GetEventPhotos(eventId), eventId= eventId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}