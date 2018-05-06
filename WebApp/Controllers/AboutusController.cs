using DAL.Entities;
using DAL.Implementation;
using System;
using System.Collections.Generic;
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

        public ActionResult GetAllStaffDetails()
        {
            try
            {
              var list=  staffDALObj.GetStaffList();
                string profileHtml = "<div id='staff{0}' class='col-lg-3 col-md-4 col-sm-6 col-xs-12 wow fadeInUp filter-teaching-staff'><div class='panel panel-default'><div class='panel-body profile'><div class='profile-image'><img src='~/images/staff{1}' alt=''></div>{6}</div><div class='panel-body'><div class='contact-info'><div class='profile-data-name'>{2}</div><div class='profile-data-title'>{3}</div><div class='profile-data-title'><span class='fa-phone fa fa-lg profile-icon'></span><span>{4}</span></div><div class='profile-data-title'><span class='fa-envelope-o fa fa-lg profile-icon'></span>{5}</div></div></div></div></div>";
                var editOption = "<span class='fa-pencil-square-o staff-edit></span>";
                for (int i = 0; i < list.Count; i++)
                    string.Format(profileHtml, list[i].Id, list[i].Name, list[i].Designation, list[i].Mobile, list[i].Email, editOption);
                return Json(new { result= profileHtml ,JsonRequestBehavior.AllowGet});
            }
            catch (Exception ex)
            {
               
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        [ValidateInput(false)]
        public ActionResult AddUpdateService(StaffDetails s)
        {
            try
            {
                staffDALObj.AddUpdate(s);
                return Json(new { result = "success", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                //WriteLogs.Write(ex);
                return Json(new { result = "error"}, JsonRequestBehavior.AllowGet);
            }

        }
    }
}