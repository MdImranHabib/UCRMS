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
    public class ClassSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassSchedules
        //public async Task<ActionResult> Index()
        //{
        //    var classSchedules = db.ClassSchedules.Include(c => c.Course).Include(c => c.Department).Include(c => c.Room);
        //    return View(await classSchedules.ToListAsync());
        //}


        // GET: ClassSchedules/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClassSchedule classSchedule = await db.ClassSchedules.FindAsync(id);
        //    if (classSchedule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classSchedule);
        //}

        // GET: ClassSchedules/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DepartmentId,CourseId,RoomId,Day,StartTime,EndTime")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                var scheduleList =await db.ClassSchedules.Where(m => m.RoomId == classSchedule.RoomId && m.Day == classSchedule.Day).ToListAsync();
                if (scheduleList.Count == 0)
                {
                    db.ClassSchedules.Add(classSchedule);
                    db.SaveChanges();
                    FlashMessage.Confirmation("Classroom has been allocated successfully!");
                }
                else
                {
                    bool status = false;
                    foreach (var schedule in scheduleList)
                    {
                        if ((classSchedule.StartTime >= schedule.StartTime && classSchedule.StartTime < schedule.EndTime)
                             || (classSchedule.EndTime > schedule.StartTime && classSchedule.EndTime <= schedule.EndTime))
                        {
                            status = true;
                        }
                    }

                    if (status == false)
                    {
                        db.ClassSchedules.Add(classSchedule);
                        db.SaveChanges();
                        FlashMessage.Confirmation("Classroom has been allocated successfully!");
                    }
                    else
                    {
                        FlashMessage.Danger("There is a conflict with the time and day. please choose another time or day.");
                    }
                }
               
                return RedirectToAction("Create");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", classSchedule.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Number", classSchedule.RoomId);
            return View(classSchedule);
        }


        public ActionResult ShowClassSchedule()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Code");
            return View();
        }


        public JsonResult GetClassSchedule(int deptId)
        {
            var classSchedules = db.ClassSchedules.Include(c => c.Course)
                .Include(c => c.Department)
                .Include(c => c.Room)
                .Where(c => c.DepartmentId == deptId).ToList();
            return Json(classSchedules, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UnAllocateAllClassrooms()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UnAllocateAllClassrooms(string status)
        {
            var allData = await db.ClassSchedules.ToListAsync();
            db.ClassSchedules.RemoveRange(allData);
            await db.SaveChangesAsync();
            FlashMessage.Confirmation("Unallocation of all classrooms is successfull!");
            return RedirectToAction("UnAllocateAllClassrooms");
        }

        // GET: ClassSchedules/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClassSchedule classSchedule = await db.ClassSchedules.FindAsync(id);
        //    if (classSchedule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", classSchedule.CourseId);
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", classSchedule.DepartmentId);
        //    ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Number", classSchedule.RoomId);
        //    return View(classSchedule);
        //}

        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,DepartmentId,CourseId,RoomId,Day,StartTime,EndTime")] ClassSchedule classSchedule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(classSchedule).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", classSchedule.CourseId);
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", classSchedule.DepartmentId);
        //    ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Number", classSchedule.RoomId);
        //    return View(classSchedule);
        //}

        // GET: ClassSchedules/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClassSchedule classSchedule = await db.ClassSchedules.FindAsync(id);
        //    if (classSchedule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classSchedule);
        //}

        // POST: ClassSchedules/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ClassSchedule classSchedule = await db.ClassSchedules.FindAsync(id);
        //    db.ClassSchedules.Remove(classSchedule);
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
