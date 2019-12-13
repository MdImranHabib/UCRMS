namespace UCRMS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UCRMS.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UCRMS.Models.ApplicationDbContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UCRMS.Models.ApplicationDbContext context)
        {
            db.Semesters.AddOrUpdate(x => x.Id,
                new Semester() { Id = 1, Name = "First Semester"},
                new Semester() { Id = 2, Name = "Second Semester"},
                new Semester() { Id = 3, Name = "Third Semester"},
                new Semester() { Id = 4, Name = "Fourth Semester"},
                new Semester() { Id = 5, Name = "Fifth Semester"},
                new Semester() { Id = 6, Name = "Sixth Semester"},
                new Semester() { Id = 7, Name = "Seventh Semester"},
                new Semester() { Id = 8, Name = "Eighth Semester"}
                );

            db.Designations.AddOrUpdate(x => x.Id,
                new Designation() { Id = 1, Name = "Professor"},
                new Designation() { Id = 2, Name = "Associate Professor"},
                new Designation() { Id = 3, Name = "Assistant Professor"},
                new Designation() { Id = 4, Name = "Senior Lecturer"},
                new Designation() { Id = 5, Name = "Lecturer" }
                );

        }
    }
}
