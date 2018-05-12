using DAL.Entities;
using DAL.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public string GetStaffListHtml()
        {
            var list = staffDALObj.GetStaffList();
            string profileHtml = "";
            for (int i = 0; i < list.Count; i++)
                profileHtml += "<div id='staff" + list[i].Id + "' class='col-lg-3 col-md-4 col-sm-6 col-xs-12 wow fadeInUp filter-teaching-staff'><div class='panel panel-default'><div class='panel-body profile'>"+(true? "<i class='fa fa-edit staff-edit-icon' data-staff-id='"+list[i].Id+"' title='Edit'></i>" : "")+ "<div class='profile-image'><img id='liSImg" + list[i].Id + "' src='/images/staff/" + list[i].Id + ".jpg' alt=''></div></div><div class='panel-body'><div class='contact-info'><div class='profile-data-name' id='liSName" + list[i].Id + "'>" + list[i].Name + "</div><div class='profile-data-title' id='liSDesignation" + list[i].Id + "'>" + list[i].Designation + "</div><div class='profile-data-title'><span class='fa-phone fa fa-lg profile-icon'></span><span id='liSMobile" + list[i].Id + "'>" + list[i].Mobile + "</span></div><div class='profile-data-title'><span class='fa-envelope-o fa fa-lg profile-icon'></span><span id='liSEmail" + list[i].Id + "'>" + list[i].Email + "</span></div></div></div></div></div>";
            return profileHtml;

        }
        public ActionResult GetAllStaffDetails()
        {
            try
            {
                return Json(new { result= "success",list=GetStaffListHtml() ,JsonRequestBehavior.AllowGet});
            }
            catch (Exception ex)
            {
               
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddUpdateStaff(StaffDetails s, HttpPostedFileBase file)
        {
            try
            {
                int staffId=0;
                var data= staffDALObj.AddUpdate(s, ref staffId);
                JsonSerializer j = new JsonSerializer();
                string newImage = Server.MapPath("~/images/staff/" + staffId + ".jpg");
                if (System.IO.File.Exists(newImage))
                    System.IO.File.Delete(newImage);

                if (s.Image.Contains("data:image/png;base64") || s.Image.Contains("data:image/jpg;base64") || s.Image.Contains("data:image/jpeg;base64"))
                {

                    string x = string.Empty;
                    if(s.Image.Contains("data:image/png;base64,"))
                        x =  s.Image.Replace("data:image/png;base64,", "");
                    else if (s.Image.Contains("data:image/jpg;base64,"))
                        x = s.Image.Replace("data:image/jpg;base64,", "");
                    else
                        x = s.Image.Replace("data:image/jpeg;base64,", "");

                    // Convert Base64 String to byte[]
                    byte[] imageBytes = Convert.FromBase64String(x);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(Server.MapPath("~/images/staff/" + staffId + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    System.IO.File.Copy(Server.MapPath("~/images/no-image.jpg"), Server.MapPath("~/images/staff/" + staffId + ".jpg"));
                }
                return Json(new { result = "success", staffId = staffId },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //WriteLogs.Write(ex);
                return Json(new { result = "error"}, JsonRequestBehavior.AllowGet);
            }

        }
    }
}