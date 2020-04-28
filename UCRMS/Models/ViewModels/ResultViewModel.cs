using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS.Models.ViewModels
{
    public class ResultViewModel
    {
        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public string Grade { get; set; }
    }
}