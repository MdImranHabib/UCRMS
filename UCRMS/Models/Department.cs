using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace UCRMS.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Input Department Code")]
        [StringLength(7,MinimumLength = 2, ErrorMessage = "Code must be 2 to 7 characters")]
        [Remote("IsCodeExist","Departments", ErrorMessage = "Department code is already exist")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please Input Department Name")]
        [Remote("IsNameExist", "Departments", ErrorMessage = "Department Name is already exist")]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}