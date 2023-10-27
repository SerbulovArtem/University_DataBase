using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Models
{
    internal class StudyingTerm
    {
        public long StudyingTermId { get; set; }
        public string UserId { get; set; }
        public int StudyingTypeId { get; set; }
        public long SpecialtyId { get; set; }
    }
}
