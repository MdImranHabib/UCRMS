using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UCRMS.Models;

namespace UCRMS.DAL
{
    public class UCRMSDBContext : DbContext
    {
        public UCRMSDBContext() : base("UCRMSDBContext")
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssign> CourseAssigns { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

    }
}