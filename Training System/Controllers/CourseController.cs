using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_System.Models;

namespace Training_System.Controllers
{
    [Authorize(Roles ="Trainer, Trainee")]
    public class CourseController : Controller
    {
        private DBModels dbmodel;
        private ApplicationDbContext db;

        public CourseController()
        {
            dbmodel = new DBModels();
            db = new ApplicationDbContext();
        }

        // GET: Course
        public ActionResult Index(int? id)
        {
            ViewBag.TraineeCount = dbmodel.Enrollments.Where(x => x.CourseId == id).Count();
            ViewBag.OfficeTrainer = dbmodel.OfficeAssigns.Where(x => x.CourseId == id).ToList();
            Course course = dbmodel.Courses.Find(id);
            return View(course);
        }
    }
}