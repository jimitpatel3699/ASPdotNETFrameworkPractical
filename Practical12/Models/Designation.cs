using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Practical12.Models
{
    public class Designation
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string DesignationName { get; set; }

        public IEnumerable<EmployeeTestThree> Employees { get; set; }

        [Display(Name = "Number of Employees")]
        public int EmployeesCount { get; set; }
    }
}