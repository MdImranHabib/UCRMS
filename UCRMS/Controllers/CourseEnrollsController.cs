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

namespace UCRMS.Controllers
{
    public class CourseEnrollsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseEnrolls
        //public async Task<ActionResult> Index()
        //{
        //    var courseEnrolls = db.CourseEnrolls.Include(c => c.Course).Include(c => c.Student);
        //    return View(await courseEnrolls.ToListAsync());
        //}

        // GET: CourseEnrolls/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CourseEnroll courseEnroll = await db.CourseEnrolls.FindAsync(id);
        //    if (courseEnroll == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(courseEnroll);
        //}

        // GET: CourseEnrolls/Create

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo");
            return View();
        }

        // POST: CourseEnrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StudentId,CourseId,EnrollDate")] CourseEnroll courseEnroll)
        {
            if (ModelState.IsValid)
            {
                var isEnrolled = db.CourseEnrolls.Any(e => e.StudentId == courseEnroll.StudentId && e.CourseId == courseEnroll.CourseId);
                if (isEnrolled == true)
                {
                    FlashMessage.Danger("Sorry! This student already enrolled in this course. Please select another course.");
                    return RedirectToAction("Create");
                }
                db.CourseEnrolls.Add(courseEnroll);
                await db.SaveChangesAsync();
                var student = db.Students.FirstOrDefault(s => s.Id == courseEnroll.StudentId);
                var course = db.Courses.FirstOrDefault(c => c.Id == courseEnroll.CourseId);
                FlashMessage.Confirmation("Student " + student.Name + " successfully enrolled in course " + course.Code);
                return RedirectToAction("Create");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", courseEnroll.StudentId);
            return View(courseEnroll);
        }

        public JsonResult GetStudentById(int stuId)
        {
            var student = db.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == stuId);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByStudentId(int stuId)
        {
            var student = db.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == stuId);
            var courses = db.Courses.Where(c => c.DepartmentId == student.DepartmentId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsStudentEnrolled(int stuId, int courseId)
        {
            bool status = false;
            var isEnrolled = db.CourseEnrolls.Any(e => e.StudentId == stuId && e.CourseId == courseId);
            if(isEnrolled == true)
            {
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        // GET: CourseEnrolls/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseEnroll courseEnroll = await db.CourseEnrolls.FindAsync(id);
            if (courseEnroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseEnroll.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", courseEnroll.StudentId);
            return View(courseEnroll);
        }

        // POST: CourseEnrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentId,CourseId,EnrollDate")] CourseEnroll courseEnroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseEnroll).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseEnroll.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", courseEnroll.StudentId);
            return View(courseEnroll);
        }

        // GET: CourseEnrolls/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseEnroll courseEnroll = await db.CourseEnrolls.FindAsync(id);
            if (courseEnroll == null)
            {
                return HttpNotFound();
            }
            return View(courseEnroll);
        }

        // POST: CourseEnrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseEnroll courseEnroll = await db.CourseEnrolls.FindAsync(id);
            db.CourseEnrolls.Remove(courseEnroll);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
