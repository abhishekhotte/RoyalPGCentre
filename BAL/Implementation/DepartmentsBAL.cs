using BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Implementation;

namespace BAL.Implementation
{
    public class DepartmentsBAL : IDepartments
    {
        DepartmentsDAL semDetails = new DepartmentsDAL();
        public object AddUpdateCourseDetails(Courses obj, ref int courseId)
        {
            return semDetails.AddUpdate(obj,out courseId);
        }

        public List<Departments> GetDepartments()
        {
            return semDetails.GetDepartmentsList();
        }

        public List<Courses> GetCourses()
        {
            return semDetails.GetCourseList();
        }

        public List<Semesters> GetSemesters()
        {
            return semDetails.GetSemestersList();
        }
    }
}
