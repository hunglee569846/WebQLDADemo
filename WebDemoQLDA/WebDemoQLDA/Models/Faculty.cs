using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoQLDA.Models
{
    public class Faculty
    {
        public string IdFaculty { get; set; }
        public string NameFaculty { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
