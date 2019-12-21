using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UCRMS.Models.ViewModels
{
    public class CourseAssignViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }

        [Display(Name = "Credit to be taken")]
        public double Credittobetaken { get; set; }

        [Display(Name = "Remaining Credit")]
        public double RemainingCredit { get; set; }

        [Required]
        [Display(Name = "Course Code")]
        public int CourseId { get; set; }

        [Display(Name ="Name")]
        public string CourseName { get; set; }

        [Display(Name ="Credit")]
        public double CourseCredit { get; set; }

        public virtual Department Department { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual Course Course { get; set; }
    }
}