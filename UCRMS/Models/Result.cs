using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class Result
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Select Student")]
        [Display(Name ="Student Reg. No")]
        public int StudentId { get; set; }

        [Required(ErrorMessage ="Please Select Course")]
        [Display(Name ="Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Select Grade")]
        [Display(Name = "Grade")]
        public int GradeId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }

        public virtual Grade Grade { get; set; }
    }
}