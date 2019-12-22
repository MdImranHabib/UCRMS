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
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var teachers = db.Teachers.Include(t => t.Department).Include(t => t.Designation);
            return View(await teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Teacher teacher = await db.Teachers.FindAsync(id);
        //    if (teacher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(teacher);
        //}

        public JsonResult IsEmailExist(string Email)
        {
            return Json(!db.Teachers.Any(x => x.Email.ToUpper() == Email.ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Address,Email,Contact,DesignationId,DepartmentId,Credittobetaken,RemainingCredit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCredit = teacher.Credittobetaken;
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
                FlashMessage.Confirmation("Teacher Named " + teacher.Name + " Saved Successfully!");
                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", teacher.DesignationId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Teacher teacher = await db.Teachers.FindAsync(id);
        //    if (teacher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", teacher.DepartmentId);
        //    ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", teacher.DesignationId);
        //    return View(teacher);
        //}

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Email,Contact,DesignationId,DepartmentId,Credittobetaken")] Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(teacher).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", teacher.DepartmentId);
        //    ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", teacher.DesignationId);
        //    return View(teacher);
        //}

        // GET: Teachers/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Teacher teacher = await db.Teachers.FindAsync(id);
        //    if (teacher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(teacher);
        //}

        // POST: Teachers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Teacher teacher = await db.Teachers.FindAsync(id);
        //    db.Teachers.Remove(teacher);
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
