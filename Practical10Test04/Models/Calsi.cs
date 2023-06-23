using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical10Test04.Models
{
    public class Calsi
    {
        [Required]
        [DisplayName("Number 1")]
        public decimal Number1 { get; set; }
        [Required]
        [DisplayName("Number 2")]
        public decimal Number2 { get; set; }
    }
}