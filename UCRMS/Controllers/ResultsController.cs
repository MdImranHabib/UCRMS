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
using UCRMS.Models.ViewModels;

namespace UCRMS.Controllers
{
    public class ResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Results
        public async Task<ActionResult> Index()
        {
            var results = db.Results.Include(r => r.Course).Include(r => r.Grade).Include(r => r.Student);
            return View(await results.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult Create()
        {
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StudentId,CourseId,GradeId")] Result result)
        {
            if (ModelState.IsValid)
            {
                var isResultExist = db.Results.Any(r => r.StudentId == result.StudentId && r.CourseId == result.CourseId);
                if(isResultExist == true)
                {
                    FlashMessage.Danger("Sorry! Result of this course has been submitted already.");
                    return RedirectToAction("Create");
                }
                db.Results.Add(result);
                await db.SaveChangesAsync();
                FlashMessage.Confirmation("Result has been saved sucessfully!");
                return RedirectToAction("Create");
            }

            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name", result.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", result.StudentId);
            return View(result);
        }

        public JsonResult GetEnrolledCoursesByStudentId(int stuId)
        {
            var enrolledCourses = db.CourseEnrolls.Where(e => e.StudentId == stuId).ToList();
            return Json(enrolledCourses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewResult()
        {
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo");
            return View();
        }

        public JsonResult GetResultsByStudentId(int stuId)
        {
            var resultList = new List<ResultViewModel>();
            
            var enrolledCourses = db.CourseEnrolls.Where(e => e.StudentId == stuId).ToList();
            var results = db.Results.Where(r => r.StudentId == stuId).ToList();

            if(enrolledCourses != null)
            {
                foreach (var enrolledCourse in enrolledCourses)
                {
                    var result = new ResultViewModel();
                    foreach (var item in results)
                    {
                        result.CourseCode = enrolledCourse.Course.Code;
                        result.CourseName = enrolledCourse.Course.Name;

                        if (item.CourseId == enrolledCourse.CourseId)
                        {
                            result.Grade = item.Grade.Name;
                            break;
                        }
                        else
                        {
                            result.Grade = "Not Graded Yet";
                        }
                    }
                    resultList.Add(result);
                }
            }          
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }

        // GET: Results/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", result.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name", result.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", result.StudentId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentId,CourseId,GradeId")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", result.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name", result.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegNo", result.StudentId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Result result = await db.Results.FindAsync(id);
            db.Results.Remove(result);
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
