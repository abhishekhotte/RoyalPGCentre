using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class DepartmentsDAL
    {
        RoyalPGCentreEntities dbEntities;
        public DepartmentsDAL()
        {
            dbEntities = new DAL.RoyalPGCentreEntities();
        }

        ~DepartmentsDAL()
        {
            dbEntities.Dispose();
        }

        public List<Courses> GetCourseList()
        {
           return dbEntities.GetCourseDetails().Select(s => new Courses() {
                Id=s.Id,
                DepartmentId=s.DepartmentId,
                SemId=s.SemId,
                Name =s.Name,
                Sem=s.Sem,
                Department=s.Department,
                Syllabus=s.SyllabusPath
            }).ToList();
        }

        public List<Departments> GetDepartmentsList()
        {
            return dbEntities.GetDepartmentsDetails().Select(c => new Departments() {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public List<Semesters> GetSemestersList()
        {
            return dbEntities.GetSemesterDetails().Select(s => new Semesters()
            {
                Id = s.Id,
                Name = s.Sem
            }).ToList();
        }

        public object AddUpdate(Courses obj, out int courseId)
        {
            courseId = 0;
            try
            {
                ObjectParameter outparameter = new ObjectParameter("courseId",typeof(int));
                dbEntities.AddCourseDetails(obj.Id, obj.Name, obj.DepartmentId, obj.SemId, obj.Syllabus, outparameter);
                courseId =Convert.ToInt32(outparameter.Value);
                return "success";
            }
            catch (Exception ex)
            {
                return "failure";
            }

        }
    }
}
