using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Implementation;
using DAL.Entities;
using System.IO;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DepartmentsController : Controller
    {
        DepartmentsBAL semBAL = new DepartmentsBAL();
        List<Departments> departments = new List<Departments>();
        List<Courses> courses = new List<Courses>();
        List<Semesters> semesters = new List<Semesters>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUpdate(Courses obj)
        {
            try
            {
                int courseId = 0;
                semBAL.AddUpdateCourseDetails(obj, ref courseId);

                HttpFileCollectionBase files = Request.Files;
                string path = Server.MapPath("~/Syllabus docs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                HttpPostedFileBase file = files[0];
                string filename = path + "/" + courseId + ".pdf";
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(path + "/" + courseId + ".pdf");
                file.SaveAs(filename);
                return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSyllabusFile(int courseId,string courseName)
        {
            string path = Server.MapPath("~/Syllabus docs/"+courseId);
            byte[] filebytes = System.IO.File.ReadAllBytes(path+".pdf");
            return File(filebytes,System.Net.Mime.MediaTypeNames.Application.Pdf, courseName+".pdf");
        }

        public ActionResult GetCoursesInfoList()
        {
            departments = semBAL.GetDepartments();
            courses = semBAL.GetCourses();
            semesters = semBAL.GetSemesters();
            return Json(new { result="success", departments = departments, courses = courses, semesters= semesters }, JsonRequestBehavior.AllowGet);
        }
    }
}