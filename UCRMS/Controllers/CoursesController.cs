using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCRMS.Models;
using Vereyon.Web;
using System.Data.Entity.Migrations;
using UCRMS.Models.ViewModels;

namespace UCRMS.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        //public async Task<ActionResult> Index()
        //{
        //    var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
        //    return View(await courses.ToListAsync());
        //}

        // GET: Courses/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = await db.Courses.FindAsync(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        public ActionResult ShowCourseStatistics()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Code");
            return View();
        }

        public JsonResult GetCourseStatistics(int deptId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCodeExist(string Code)
        {
            return Json(!db.Courses.Any(x => x.Code.Trim().ToUpper() == Code.Trim().ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameExist(string Name)
        {
            return Json(!db.Courses.Any(x => x.Name.Trim().ToUpper() == Name.Trim().ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name,Credit,Description,AssignTo,Status,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.AssignTo = "Not Assigned Yet";
                course.Status = false;
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                FlashMessage.Confirmation(course.Name + " Course saved Successfully!");
                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", course.SemesterId);
            return View(course);
        }

        public ActionResult AssignCourse()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignCourse([Bind(Include = "Id,DepartmentId,TeacherId,Credittobetaken,RemainingCredit,CourseId,CourseName,CourseCredit")] CourseAssignViewModel courseAssignViewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = db.Teachers.FirstOrDefault(x => x.Id == courseAssignViewModel.TeacherId);
                var course = db.Courses.FirstOrDefault(x => x.Id == courseAssignViewModel.CourseId);

                if (course.Status == true)
                {
                    FlashMessage.Danger("This Course already has been Assigned");
                }
                else
                {                   
                    teacher.RemainingCredit = teacher.RemainingCredit - course.Credit;
                    db.Teachers.AddOrUpdate(teacher);
                    await db.SaveChangesAsync();

                    course.Status = true;
                    course.AssignTo = teacher.Name;
                    db.Courses.AddOrUpdate(course);
                    await db.SaveChangesAsync();

                    FlashMessage.Confirmation("The Course " + course.Name + " Successfully Assigned to " + teacher.Name);
                }

                return RedirectToAction("AssignCourse");
            }

            ViewBag.Departments = new SelectList(db.Departments, "Id", "Code");
            return View(courseAssignViewModel);
        }

        public JsonResult GetTeachersByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(x => x.DepartmentId == deptId).ToList();
            return Json(teachers);
        }

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == deptId).ToList();
            return Json(courses);
        }

        public JsonResult GetTeachersById(int id)
        {
            var teacher = db.Teachers.Where(x => x.Id == id).FirstOrDefault();
            return Json(teacher);
        }

        public JsonResult GetCoursesById(int id)
        {
            var course = db.Courses.Where(x => x.Id == id).FirstOrDefault();
            return Json(course);
        }

        public ActionResult UnAssignAllCourses()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UnAssignAllCourses(string status)
        {
            var courses = await db.Courses.ToListAsync();
            var teachers = await db.Teachers.ToListAsync();

            if (courses == null)
            {
                FlashMessage.Danger("There is no course to unassign!");
                return RedirectToAction("UnAssignAllCourses");
            }
            foreach (var course in courses)
            {
                if(course.Status == false)
                {
                    continue;
                }
                course.Status = false;
                course.AssignTo = "Not Assigned Yet";
                db.Courses.AddOrUpdate(course);
                await db.SaveChangesAsync();
            }

            foreach(var teacher in teachers)
            {
                if(teacher.RemainingCredit == teacher.Credittobetaken)
                {
                    continue;
                }
                teacher.RemainingCredit = teacher.Credittobetaken;
                db.Teachers.AddOrUpdate(teacher);
                await db.SaveChangesAsync();
            }

            FlashMessage.Confirmation("Unassign all the courses is successfull!");
            return RedirectToAction("UnAssignAllCourses");
        }

        // GET: Courses/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = await db.Courses.FindAsync(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", course.DepartmentId);
        //    ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", course.SemesterId);
        //    return View(course);
        //}

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name,Credit,Description,DepartmentId,SemesterId")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(course).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", course.DepartmentId);
        //    ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", course.SemesterId);
        //    return View(course);
        //}

        // GET: Courses/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = await db.Courses.FindAsync(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        // POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Course course = await db.Courses.FindAsync(id);
        //    db.Courses.Remove(course);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
