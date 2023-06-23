using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practical15Test02.Models
{
    public class UserMaster
    {
        public int Id { get; set; }
        [Required,MaxLength(20)]
        public string Name { get; set; }
        [Required,MaxLength(20)]
        public string UserId { get; set; }
        [Required,MaxLength(20)]
        public string Password { get; set; }
        public bool Isactive { get; set; }
        public DateTime Createdon { get; set; }
        public Guid RoleMasterId { get; set; }
        [ForeignKey("RoleMasterId")]
        public RoleMaster RoleMaster { get; set; }
    }
}