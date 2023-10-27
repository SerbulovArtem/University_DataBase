using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Models
{
    internal class Specialtie
    {
        public long SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public long DepartmentId { get; set; }
    }
}
