using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical13Test02.Models
{
    public class EmployeeDesignation
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Designation { get; set; }
    }
}