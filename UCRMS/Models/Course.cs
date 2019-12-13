using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace UCRMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Input Course Code")]
        [MinLength(5,ErrorMessage = "Code must be at least 5 characters")]
        [Remote("IsCodeExist", "Courses", ErrorMessage = "Course code is already exist")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please Input Course Name")]
        [Remote("IsNameExist", "Courses", ErrorMessage = "Course Name is already exist")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Input Course Credit")]
        [Range(0.5, 5.0, ErrorMessage="Credit can not be less than 0.5 and more than 5.0")]
        public double Credit { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please Select Semester")]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Semester Semester { get; set; }


    }
}