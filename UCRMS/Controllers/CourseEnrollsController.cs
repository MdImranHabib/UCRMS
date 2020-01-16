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
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
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
                db.CourseEnrolls.Add(courseEnroll);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseEnroll.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", courseEnroll.StudentId);
            return View(courseEnroll);
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
