using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Training_System.Models;

namespace Training_System.Controllers
{
    [Authorize(Roles ="Trainer")]
    public class TrainerController : Controller
    {
        private DBModels dbmodel;
        private ApplicationDbContext db;


        public TrainerController()
        {
            db = new ApplicationDbContext();
            dbmodel = new DBModels();
        }

        // GET: /Trainer
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Trainer/myCourses
        public ActionResult myCourses(string id)
        {
            //string id = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(id)
            //var myid = (from g in dbmodel.OfficeAssigns where g.TrainerId == id select g).First();
            ////var myid = dbmodel.OfficeAssigns.First(p => p.TrainerId == id);
            ////OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(myid);
            var courseId = dbmodel.OfficeAssigns.Where(x => x.TrainerId.Contains(id)).Select(x => x.CourseId).FirstOrDefault();
            var officeAssign = dbmodel.OfficeAssigns.Where(x => x.TrainerId.Contains(id));
            if (officeAssign == null)
            {
                return HttpNotFound();
            }
            return View(officeAssign);
        }

        // GET: /Trainer/TrainerProfile
        public ActionResult TrainerProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = dbmodel.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrainerProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,AspNetUserId,Education")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainer).State = EntityState.Modified;
                dbmodel.SaveChanges();
                string url = "/TrainerProfile/" + trainer.Id;
                return RedirectToAction(url, "Trainer");
            }
            return View(trainer);
        }


        // GET: /Trainer/ViewCourse
        public ActionResult ViewCourse()
        {
            return View();
        }

        // GET: /Trainer/MarkStudents
        public ActionResult MarkStudents(int? id)
        {
            var trainee_id = dbmodel.Scores.Where(x => x.CourseId == id).Select(x => x.TraineeId).FirstOrDefault();
            ViewBag.ScoreId = dbmodel.Scores.Where(x => x.TraineeId == trainee_id).ToList();
            var enrollments = dbmodel.Enrollments.Include(e => e.Course).Include(e => e.Trainee).Where(s => s.CourseId == id);
            return View(enrollments.ToList());
        }

        // GET: Scores/Details/5
        public ActionResult DetailsScore(string Trainee_Id, int? Course_Id)
        {
            if (Trainee_Id == null || Course_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Trainee_Id = Trainee_Id;
            ViewBag.Course_Id = Course_Id;
            var scoreid = dbmodel.Scores.Where(x => x.TraineeId.Contains(Trainee_Id)).Select(x => x.Id).First();
            Score score = dbmodel.Scores.Find(scoreid);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // GET: /Trainer/AssignScore
        public ActionResult AssignScore(string Trainee_Id, int? Course_Id)
        {
            if (Trainee_Id == null || Course_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name");
            ViewBag.TraineeId = new SelectList(dbmodel.Trainees, "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignScore([Bind(Include = "Id,Mark_Date,ToTal,Status,CourseId,TraineeId,Update_Date")] Score score)
        {
            var scores = dbmodel.Scores.Where(x => x.TraineeId.Contains(score.TraineeId)).FirstOrDefault();
            if (ModelState.IsValid)
            {
                dbmodel.Scores.Add(score);
                dbmodel.SaveChanges();
                string course = "/MarkStudents/" + score.CourseId.ToString();
                return RedirectToAction(course, "Trainer");
            }
            return View(score);
        }

        // GET: Scores/Edit/5
        public ActionResult EditScore(string Trainee_Id, int? Course_Id)
        {
            if (Trainee_Id == null || Course_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Course_Id = Course_Id;
            ViewBag.Trainee_Id = Trainee_Id;
            var scoreid = dbmodel.Scores.Where(x => x.TraineeId.Contains(Trainee_Id)).Select(x => x.Id).FirstOrDefault();
            Score score = dbmodel.Scores.Find(scoreid);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditScore([Bind(Include = "Id,Mark_Date,ToTal,Status,CourseId,TraineeId,Update_Date")] Score score)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(score).State = EntityState.Modified;
                dbmodel.SaveChanges();
                string url = "/MarkStudents/" + score.CourseId.ToString();
                return RedirectToAction(url,"Trainer");
            }
            return View(score);
        }

        // GET: Scores/Delete/5
        public ActionResult DeleteScore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = dbmodel.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("DeleteScore")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteScoreConfirm(int id)
        {
            Score score = dbmodel.Scores.Find(id);
            dbmodel.Scores.Remove(score);
            dbmodel.SaveChanges();
            string course = "/MarkStudents/" + score.CourseId.ToString();
            return RedirectToAction(course, "Trainer");
        }
    }
}