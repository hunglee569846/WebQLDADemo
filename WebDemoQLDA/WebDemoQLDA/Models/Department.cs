using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoQLDA.Models
{
    public class Department
    {
        public string IdDepartment { get; set; }
        public string NameDepartment { get; set; }
        public string Office { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdFaculty { get; set; }
        public string NameFaculty { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime LastUpdate { get; set; }
    }
}
