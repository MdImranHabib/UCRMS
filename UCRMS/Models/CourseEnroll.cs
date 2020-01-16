using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class CourseEnroll
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select Student")]
        public int StudentId { get; set; }
       
        [Required(ErrorMessage = "Please Select Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage ="Please Select a Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollDate { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }
}