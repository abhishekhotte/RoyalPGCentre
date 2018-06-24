using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Courses
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int SemId { get; set; }
        public string Name { get; set; }
        public string Sem { get; set; }
        public string Department { get; set; }
        public string Syllabus { get; set; }
    }
}
