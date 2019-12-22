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
