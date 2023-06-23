using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical11Test01.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Mobile number required")]
        [StringLength(10,MinimumLength =10)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }
        
    }
}