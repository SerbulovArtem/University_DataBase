using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Models
{
    internal class Grade
    {
        public long GradeId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public int TutorId { get; set; }
        public int SubjectId { get; set; }
    }
}
