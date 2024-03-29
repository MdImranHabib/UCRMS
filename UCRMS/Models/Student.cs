﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCRMS.Models
{
    public class Student
    {
        public int Id { get; set; }

        //[Required]
        public string RegNo { get; set; }

        [Required(ErrorMessage ="Please Input Student Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Input Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email")]
        [Remote("IsEmailExist", "Students", ErrorMessage ="This email is already in use. Please use another")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Input Contact No.")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [StringLength(14, MinimumLength = 7, ErrorMessage = "Invalid Phone Number")]
        [Display(Name="Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Select Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage ="Please Select Department")]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

    }
}