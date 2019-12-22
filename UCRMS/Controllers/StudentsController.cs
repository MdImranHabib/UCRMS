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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(await students.ToListAsync());
        }

        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = await db.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        public JsonResult IsEmailExist (string Email)
        {
            return Json(!db.Students.Any(x => x.Email.ToUpper() == Email.ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RegNo,Name,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        {
           
            if (ModelState.IsValid)
            {
                student.RegNo = GetRegNo(student);
                db.Students.Add(student);
                await db.SaveChangesAsync();
                //FlashMessage.Confirmation("Registration Successfull. Your Registration number is: " + student.RegNo);
                return View("Details",student);
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", student.DepartmentId);
            return View(student);
        }

        public string GetRegNo(Student student)
        {
            var studentCount = db.Students.Count(x => (x.DepartmentId == student.DepartmentId) && (x.Date.Year == student.Date.Year)) + 1;

            var department = db.Departments.FirstOrDefault(x => x.Id == student.DepartmentId);

            string leadingZero = "";
            int length = 3 - studentCount.ToString().Length;
            for (int i = 0; i < length; i++)
            {
                leadingZero += "0";
            }

            string studentRegNo = department.Code + "-" + student.Date.Year + "-" + leadingZero + studentCount;
            return studentRegNo;
        }

        // GET: Students/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = await db.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", student.DepartmentId);
        //    return View(student);
        //}

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,RegNo,Name,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", student.DepartmentId);
        //    return View(student);
        //}

        // GET: Students/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = await db.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Student student = await db.Students.FindAsync(id);
        //    db.Students.Remove(student);
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
