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
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                obj.Syllabus = file.FileName;
                int courseId = 0;
                semBAL.AddUpdateCourseDetails(obj, ref courseId);
                string fileExt = file.FileName.Split('.').LastOrDefault();
                string path = Server.MapPath("~/Syllabus docs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string filename = path + "\\" + courseId + "."+ fileExt;
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
                file.SaveAs(filename);
                return Json(new { result = "success", courseId= courseId , syllabus= obj.Syllabus}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateCourse(Courses obj)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                obj.Syllabus = file.FileName;
                int courseId = 0;
                semBAL.AddUpdateCourseDetails(obj, ref courseId);

                string path = Server.MapPath("~/Syllabus docs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string filename = path + "/" + courseId + ".pdf";
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(path + "/" + courseId + ".pdf");
                file.SaveAs(filename);
                return Json(new { result = "success", courseId = courseId, syllabus = obj.Syllabus }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSyllabusFile(int? courseId,string syllabus)
        {
            string fileExt = syllabus.Split('.').LastOrDefault();

            string path = Server.MapPath("~/Syllabus docs/"+ courseId+"."+ fileExt);
            if (System.IO.File.Exists(path))
            {
                byte[] filebytes = System.IO.File.ReadAllBytes(path);
                return File(filebytes, System.Net.Mime.MediaTypeNames.Application.Octet, syllabus);
            }
            else
            {
                return Content("<script>alert('Syllabus file not found')</script>");

            }
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