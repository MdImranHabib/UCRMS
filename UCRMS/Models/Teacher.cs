using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Credit to be taken must be non-negative value")]
        [Display(Name ="Credit to be taken")]
        public double Credittobetaken { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<CourseAssign> CourseAssigns { get; set; }

    }
}