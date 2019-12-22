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
using UCRMS.Models.ViewModels;
using Vereyon.Web;
using System.Data.Entity.Migrations;

namespace UCRMS.Controllers
{
    public class CourseAssignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var courseAssigns = db.CourseAssigns.Include(c => c.Course).Include(c => c.Teacher);
            return View(await courseAssigns.ToListAsync());
        }

        // GET: CourseAssigns/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
        //    if (courseAssign == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(courseAssign);
        //}

        public JsonResult GetTeachersByDeptId(int id)
        {
            var teachers = db.Teachers.Where(x => x.DepartmentId == id).ToList();
            return Json(teachers,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByDeptId(int id)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == id).ToList();
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

        public ActionResult Create()
        {
            var departments = db.Departments.ToList();
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DepartmentId,TeacherId,Credittobetaken,RemainingCredit,CourseId,CourseName,CourseCredit")] CourseAssignViewModel courseAssignViewModel)
        {
            if (ModelState.IsValid)
            {
                if (db.CourseAssigns.Any(x => x.CourseId == courseAssignViewModel.CourseId))
                {
                    FlashMessage.Danger("This Course already has been Assigned");
                }
                else
                {
                    CourseAssign courseAssign = new CourseAssign()
                    {
                        TeacherId = courseAssignViewModel.TeacherId,
                        CourseId = courseAssignViewModel.CourseId
                    };

                    db.CourseAssigns.Add(courseAssign);
                    await db.SaveChangesAsync();

                    var teacher = db.Teachers.FirstOrDefault(x => x.Id == courseAssignViewModel.TeacherId);
                    var course = db.Courses.FirstOrDefault(x => x.Id == courseAssignViewModel.CourseId);

                    teacher.RemainingCredit = teacher.RemainingCredit - course.Credit;
                    db.Teachers.AddOrUpdate(teacher);
                    await db.SaveChangesAsync();

                    course.Status = true;
                    course.AssignTo = teacher.Name;
                    db.Courses.AddOrUpdate(course);
                    await db.SaveChangesAsync();

                    FlashMessage.Confirmation("The Course " + course.Name + " Successfully Assigned to " + teacher.Name);
                }
               
                return RedirectToAction("Create");
            }

            ViewBag.Departmens = new SelectList(db.Departments, "Id", "Code");
            return View(courseAssignViewModel);
        }

        // GET: CourseAssigns/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
        //    if (courseAssign == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseAssign.CourseId);
        //    ViewBag.Departmens = new SelectList(db.Departments, "Id", "Code");
        //    ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseAssign.TeacherId);
        //    return View(courseAssign);
        //}

        // POST: CourseAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,DepartmentId,TeacherId,CourseId,IsAssigned")] CourseAssign courseAssign)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(courseAssign).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseAssign.CourseId);
        //    ViewBag.Departmens = new SelectList(db.Departments, "Id", "Code");
        //    ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseAssign.TeacherId);
        //    return View(courseAssign);
        //}

        // GET: CourseAssigns/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
        //    if (courseAssign == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(courseAssign);
        //}

        // POST: CourseAssigns/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
        //    db.CourseAssigns.Remove(courseAssign);
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
