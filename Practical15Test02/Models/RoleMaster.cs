using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical15Test02.Models
{
    public class RoleMaster
    {
        [Key]
        public Guid Id { get; set; }
        [Required,MaxLength(20)]
        public string RoleDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}