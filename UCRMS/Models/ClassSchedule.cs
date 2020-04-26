using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class ClassSchedule
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Room No.")]
        public int RoomId { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "From")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "To")]
        public DateTime EndTime { get; set; }

        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }
    }
}