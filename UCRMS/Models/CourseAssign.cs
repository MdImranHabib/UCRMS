using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UCRMS.Models
{
    public class CourseAssign
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual Course Course { get; set; }
    }
}