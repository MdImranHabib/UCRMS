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
            //context.Semesters.AddOrUpdate(x => x.Id,
            //    new Semester() { Name = "First Semester"},
            //    new Semester() { Name = "Second Semester"},
            //    new Semester() { Name = "Third Semester"},
            //    new Semester() { Name = "Fourth Semester"},
            //    new Semester() { Name = "Fifth Semester"},
            //    new Semester() { Name = "Sixth Semester"},
            //    new Semester() { Name = "Seventh Semester"},
            //    new Semester() { Name = "Eighth Semester"}
            //    );

            //context.Designations.AddOrUpdate(
            //    new Designation() { Name = "Professor"},
            //    new Designation() { Name = "Associate Professor"},
            //    new Designation() { Name = "Assistant Professor"},
            //    new Designation() { Name = "Senior Lecturer"},
            //    new Designation() { Name = "Lecturer" }
            //    );

            //context.Grades.AddOrUpdate(x => x.Id,
            //  new Grade() { Name = "A+" },
            //  new Grade() { Name = "A" },
            //  new Grade() { Name = "A-" },
            //  new Grade() { Name = "B+" },
            //  new Grade() { Name = "B" },
            //  new Grade() { Name = "B-" },
            //  new Grade() { Name = "C+" },
            //  new Grade() { Name = "C" },
            //  new Grade() { Name = "C-" },
            //  new Grade() { Name = "D+" },
            //  new Grade() { Name = "D" },
            //  new Grade() { Name = "D-" },
            //  new Grade() { Name = "F" }
            //  );

            //context.Rooms.AddOrUpdate(
            //    new Room() { Number = "101" },
            //    new Room() { Number = "102" },
            //    new Room() { Number = "103" },
            //    new Room() { Number = "201" },
            //    new Room() { Number = "202" },
            //    new Room() { Number = "203" },
            //    new Room() { Number = "301" },
            //    new Room() { Number = "302" },
            //    new Room() { Number = "303" },
            //    new Room() { Number = "304" }
            //    );
        }
    }
}
