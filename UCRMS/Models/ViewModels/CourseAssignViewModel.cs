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
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public string Name { get; set; }

        public double Credit { get; set; }

        public Department Department { get; set; }

        public Teacher Teacher { get; set; }

        public Course Course { get; set; }
    }
}