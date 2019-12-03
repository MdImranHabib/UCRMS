using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCRMS.DAL;
using UCRMS.Models;

namespace UCRMS.Controllers
{
    public class CourseAssignsController : Controller
    {
        private UCRMSDBContext db = new UCRMSDBContext();

        // GET: CourseAssigns
        public async Task<ActionResult> Index()
        {
            var courseAssigns = db.CourseAssigns.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(await courseAssigns.ToListAsync());
        }

        // GET: CourseAssigns/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // GET: CourseAssigns/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: CourseAssigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseAssign);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // POST: CourseAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssign).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // POST: CourseAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseAssign courseAssign = await db.CourseAssigns.FindAsync(id);
            db.CourseAssigns.Remove(courseAssign);
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
