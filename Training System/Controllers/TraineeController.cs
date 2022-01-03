using Microsoft.AspNet.Identity;
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
    [Authorize(Roles ="Trainee")]
    public class TraineeController : Controller
    {
        private DBModels dbmodel;
        private ApplicationDbContext db;

        public TraineeController()
        {
            db = new ApplicationDbContext();
            dbmodel = new DBModels();
        }

        // GET: /Trainee
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Trainee/TraineeProfile
        public ActionResult TraineeProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = dbmodel.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TraineeProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,ProgrammingLanguage,TOEICscore,AspNetUserId,Education")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainee).State = EntityState.Modified;
                dbmodel.SaveChanges();
                string url = "/TraineeProfile/" + trainee.Id;
                return RedirectToAction(url, "Trainee");
            }
            return View(trainee);
        }

        // GET: /Trainee/myCourses
        public ActionResult myCourses(string id)
        {
            //string id = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enrollment = dbmodel.Enrollments.Where(x => x.TraineeId.Contains(id));
            var courseId = dbmodel.Enrollments.Where(x => x.TraineeId.Contains(id)).Select(x => x.CourseId).FirstOrDefault();
            ViewBag.OfficeTrainer = dbmodel.OfficeAssigns.Where(x => x.CourseId == courseId).ToList();
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: /Trainee/ViewCourse
        public ActionResult ViewCourse()
        {
            return View();
        }

        // GET: /Trainee/ScoreResult
        public ActionResult ScoreResult(int? id, string Trainee_Id)
        {
            if(id == null || Trainee_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var scoreid = dbmodel.Scores.Where(s => s.TraineeId == Trainee_Id).Select(s => s.Id).FirstOrDefault();
            var courseid = dbmodel.Scores.Where(s => s.TraineeId == Trainee_Id).Select(s => s.CourseId).FirstOrDefault();
            if (courseid == id)
            {
                //Score score = dbmodel.Scores.Find(scoreid);
                var scores = dbmodel.Scores.Include(e => e.Course).Include(e => e.Trainee).Where(s => s.Id == scoreid);
                if (scores == null)
                {
                    return HttpNotFound();
                }
                return View(scores.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}