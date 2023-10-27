using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Models
{
    public class Subject
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long DepartmentId { get; set; }
    }
}
