using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    interface IDepartments
    {
        List<Courses> GetCourses();
        List<Departments> GetDepartments();
        List<Semesters> GetSemesters();
        object AddUpdateCourseDetails(Courses obj, ref int courseId);
    }
}
