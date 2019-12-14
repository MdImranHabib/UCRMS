using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCRMS.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Input Teacher Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Input Email")]
        [EmailAddress]
        [Remote("IsEmailExist", "Teachers", ErrorMessage ="This email is already in use. Please try another.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Input Contact")]
        [Phone]
        [Display(Name ="Contact No.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Please Select Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please Input Credit to be taken")]
        [Range(0, double.MaxValue, ErrorMessage = "Credit to be taken must be non-negative value")]
        [Display(Name ="Credit to be taken")]
        public double Credittobetaken { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<CourseAssign> CourseAssigns { get; set; }

    }
}