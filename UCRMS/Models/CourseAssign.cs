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

        [Required(ErrorMessage = "Please Select Teacher")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please Select Course")]
        [Display(Name = "Course Code")]
        public int CourseId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual Course Course { get; set; }
    }
}