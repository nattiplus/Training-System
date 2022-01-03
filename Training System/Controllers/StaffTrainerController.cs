using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Training_System.Models;

namespace Training_System.Controllers
{
    [Authorize(Roles ="Staff Trainer")]
    public class StaffTrainerController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext db;
        private DBModels dbmodel;

        public StaffTrainerController()
        {
            db = new ApplicationDbContext();
            dbmodel = new DBModels();
        }

        public StaffTrainerController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: /StaffTrainer
        public ActionResult Index(string id)
        {
            var users = db.Users.Include(c => c.Roles);
            ViewBag.Users = users.ToList();
            return View();
        }

        // GET: Admin/DetailsAccount/5
        // GET: ApplicationUsers/DetailsAccount/5
        public ActionResult DetailsAccount(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userDetails = db.Users.Find(id);
                if (userDetails == null)
                {
                    return HttpNotFound();
                }
                return View(userDetails);
            }
        }

        // GET: ApplicationUsers/CreateAccount
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();
        }


        // GET: ApplicationUsers/EditAccount/5
        public ActionResult EditAccount(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser applicationUser = db.Users.Find(id);
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                return View(applicationUser);
            }
        }

        // POST: ApplicationUsers/EditAccount/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(applicationUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(applicationUser);
            }
        }

        // GET: ApplicationUsers/DeleteAccount/5
        public ActionResult DeleteAccount(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser applicationUser = db.Users.Find(id);
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                return View(applicationUser);
            }
        }

        // POST: ApplicationUsers/DeleteAccount/5
        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountConfirmed(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                db.Users.Remove(applicationUser);
                db.SaveChanges();
                return RedirectToAction("manageAccount");
            }
        }

        // GET: /StaffTrainer/StaffProfile
        public ActionResult StaffProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingStaff trainingStaff = dbmodel.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StaffProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,AspNetUserId,Education")] TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainingStaff).State = EntityState.Modified;
                dbmodel.SaveChanges();
                string url = "/StaffProfile/" + trainingStaff.Id;
                return RedirectToAction(url, "StaffTrainer");
            }
            return View(trainingStaff);
        }

        // GET: /StaffTrainer/manageTrainee
        public ActionResult manageTrainer()
        {
            var trainers = dbmodel.Trainers.Include(t => t.AspNetUser);
            return View(trainers.ToList());
        }

        // GET: Trainers/Details/5
        public ActionResult DetailsTrainerProfile(string id)
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

        // GET: Trainers/Create
        public ActionResult AssignTrainerProfile()
        {
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTrainerProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,AspNetUserId,Education")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                trainer.Id = trainer.AspNetUserId;
                dbmodel.Trainers.Add(trainer);
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainer");
            }

            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainer.AspNetUserId);
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public ActionResult EditTrainerProfile(string id)
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
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainer.AspNetUserId);
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainerProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,AspNetUserId,Education")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainer).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainer");
            }
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainer.AspNetUserId);
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public ActionResult DeleteTrainerProfile(string id)
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

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("DeleteTrainerProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrainerProfileConfirmed(string id)
        {
            Trainer trainer = dbmodel.Trainers.Find(id);
            dbmodel.Trainers.Remove(trainer);
            dbmodel.SaveChanges();
            return RedirectToAction("manageTrainer");
        }

        // GET: /StaffTrainer/manageTrainee
        public ActionResult manageTrainee()
        {
            var trainees = dbmodel.Trainees.Include(t => t.AspNetUser);
            return View(trainees.ToList());
        }

        // GET: Trainees/Details/5
        public ActionResult DetailsTraineeProfile(string id)
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

        // GET: Trainees/Create
        public ActionResult AssignTraineeProfile()
        {
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTraineeProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,ProgrammingLanguage,TOEICscore,AspNetUserId,Education")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                trainee.Id = trainee.AspNetUserId;
                dbmodel.Trainees.Add(trainee);
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainee");
            }

            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainee.AspNetUserId);
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public ActionResult EditTraineeProfile(string id)
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
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainee.AspNetUserId);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTraineeProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,ProgrammingLanguage,TOEICscore,AspNetUserId,Education")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainee).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainee");
            }
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainee.AspNetUserId);
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        public ActionResult DeleteTraineeProfile(string id)
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

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("DeleteTraineeProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTraineeProfileConfirmed(string id)
        {
            Trainee trainee = dbmodel.Trainees.Find(id);
            dbmodel.Trainees.Remove(trainee);
            dbmodel.SaveChanges();
            return RedirectToAction("manageTrainee");
        }

        // GET: /StaffTrainer/manageCategory
        public ActionResult manageCategory()
        {
            ViewBag.Categories = dbmodel.Categories.ToList();
            return View();
        }

        // GET: /StaffTrainer/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "Id,Name,Image,Create_Date,Update_Date")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                category.Image = "~/Content/img/" + filename;
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                ImageFile.SaveAs(filename);
            }

            if (ModelState.IsValid)
            {
                dbmodel.Categories.Add(category);
                dbmodel.SaveChanges();
                return RedirectToAction("manageCategory");
            }
            ModelState.Clear();
            return View(category);
        }

        // GET: Categories/Details/5
        public ActionResult DetailsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = dbmodel.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = dbmodel.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "Id,Name,Image,Create_Date,Update_Date")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    dbmodel.Entry(category).State = EntityState.Modified;
                    dbmodel.SaveChanges();
                    return RedirectToAction("manageCategory");
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    category.Image = "~/Content/img/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    ImageFile.SaveAs(filename);
                    dbmodel.Entry(category).State = EntityState.Modified;
                    dbmodel.SaveChanges();
                    return RedirectToAction("manageCategory");
                }

            }
            ModelState.Clear();
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = dbmodel.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            Category category = dbmodel.Categories.Find(id);
            dbmodel.Categories.Remove(category);
            dbmodel.SaveChanges();
            return RedirectToAction("manageCategory");
        }

        // GET: /StaffTrainer/manageCourse
        public ActionResult manageCourse()
        {
            var courses = dbmodel.Courses.Include(c => c.Category);
            ViewBag.Courses = courses.ToList();
            return View();
        }

        // GET: Courses/CreateCourse
        public ActionResult CreateCourse()
        {
            ViewBag.CategoryId = new SelectList(dbmodel.Categories, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse([Bind(Include = "Id,Name,Descriptions,Requirements,Image,Publisher,About,TotalTime,Create_Date,Update_Date,CategoryId")] Course course, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                course.Image = "~/Content/img/" + filename;
                filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                ImageFile.SaveAs(filename);
            }

            if (ModelState.IsValid)
            {
                dbmodel.Courses.Add(course);
                dbmodel.SaveChanges();
                return RedirectToAction("manageCourse");
            }
            ViewBag.CategoryId = new SelectList(dbmodel.Categories, "Id", "Name", course.CategoryId);
            ModelState.Clear();
            return View(course);
        }

        // GET: Courses/Details/5
        public ActionResult DetailsCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = dbmodel.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult EditCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = dbmodel.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(dbmodel.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "Id,Name,Descriptions,Requirements,Image,Publisher,About,TotalTime,Create_Date,Update_Date,CategoryId")] Course course, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    dbmodel.Entry(course).State = EntityState.Modified;
                    dbmodel.SaveChanges();
                    return RedirectToAction("manageCourse");
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    course.Image = "~/Content/img/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    ImageFile.SaveAs(filename);
                    dbmodel.Entry(course).State = EntityState.Modified;
                    dbmodel.SaveChanges();
                    return RedirectToAction("manageCourse");
                }
            }
            ViewBag.CategoryId = new SelectList(dbmodel.Categories, "Id", "Name", course.CategoryId);
            ModelState.Clear();
            return View(course);
        }

        // GET: Courses/DeleteCourse/5
        public ActionResult DeleteCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = dbmodel.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/DeleteCourse/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourseConfirmed(int id)
        {
            Course course = dbmodel.Courses.Find(id);
            dbmodel.Courses.Remove(course);
            dbmodel.SaveChanges();
            return RedirectToAction("manageCourse");
        }

        // GET: /StaffTrainer/manageEnrollment
        public ActionResult manageEnrollment()
        {
            var enrollments = dbmodel.Enrollments.Include(e => e.Course).Include(e => e.Trainee);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult DetailsEnrollment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = dbmodel.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult CreateEnrollment()
        {
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name");
            ViewBag.TraineeId = new SelectList(dbmodel.Trainees, "Id", "FullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEnrollment([Bind(Include = "Id,Enroll_Date,Descriptions,Status,CourseId,TraineeId,Update_Date")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Enrollments.Add(enrollment);
                dbmodel.SaveChanges();
                return RedirectToAction("manageEnrollment");
            }

            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", enrollment.CourseId);
            ViewBag.TraineeId = new SelectList(dbmodel.Trainees, "Id", "FullName", enrollment.TraineeId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult EditEnrollment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = dbmodel.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", enrollment.CourseId);
            ViewBag.TraineeId = new SelectList(dbmodel.Trainees, "Id", "FullName", enrollment.TraineeId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEnrollment([Bind(Include = "Id,Enroll_Date,Descriptions,Status,CourseId,TraineeId,Update_Date")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(enrollment).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageEnrollment");
            }
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", enrollment.CourseId);
            ViewBag.TraineeId = new SelectList(dbmodel.Trainees, "Id", "FullName", enrollment.TraineeId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult DeleteEnrollment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = dbmodel.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("DeleteEnrollment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEnrollmentConfirmed(int id)
        {
            Enrollment enrollment = dbmodel.Enrollments.Find(id);
            dbmodel.Enrollments.Remove(enrollment);
            dbmodel.SaveChanges();
            return RedirectToAction("manageEnrollment");
        }

        // GET: StaffTrainer/manageOfficeAssign
        public ActionResult manageOfficeAssign()
        {
            var officeAssigns = dbmodel.OfficeAssigns.Include(o => o.Course).Include(o => o.Trainer);
            return View(officeAssigns.ToList());
        }

        // GET: OfficeAssigns/Details/5
        public ActionResult DetailsOfficeAssign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(id);
            if (officeAssign == null)
            {
                return HttpNotFound();
            }
            return View(officeAssign);
        }

        // GET: OfficeAssigns/Create
        public ActionResult CreateOfficeAssign()
        {
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name"); ;
            ViewBag.TrainerId = new SelectList(dbmodel.Trainers, "Id", "FullName");
            return View();
        }

        // POST: OfficeAssigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOfficeAssign([Bind(Include = "Id,Assign_Date,Update_Date,CourseId,Status,TrainerId")] OfficeAssign officeAssign)
        {
            if (ModelState.IsValid)
            {
                dbmodel.OfficeAssigns.Add(officeAssign);
                dbmodel.SaveChanges();
                return RedirectToAction("manageOfficeAssign");
            }

            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", officeAssign.CourseId);
            ViewBag.TrainerId = new SelectList(dbmodel.Trainers, "Id", "FullName", officeAssign.TrainerId);
            return View(officeAssign);
        }

        // GET: OfficeAssigns/Edit/5
        public ActionResult EditOfficeAssign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(id);
            if (officeAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", officeAssign.CourseId);
            ViewBag.TrainerId = new SelectList(dbmodel.Trainers, "Id", "FullName", officeAssign.TrainerId);
            return View(officeAssign);
        }

        // POST: OfficeAssigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOfficeAssign([Bind(Include = "Id,Assign_Date,Update_Date,CourseId,Status,TrainerId")] OfficeAssign officeAssign)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(officeAssign).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageOfficeAssign");
            }
            ViewBag.CourseId = new SelectList(dbmodel.Courses, "Id", "Name", officeAssign.CourseId);
            ViewBag.TrainerId = new SelectList(dbmodel.Trainers, "Id", "FullName", officeAssign.TrainerId);
            return View(officeAssign);
        }

        // GET: OfficeAssigns/Delete/5
        public ActionResult DeleteOfficeAssign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(id);
            if (officeAssign == null)
            {
                return HttpNotFound();
            }
            return View(officeAssign);
        }

        // POST: OfficeAssigns/Delete/5
        [HttpPost, ActionName("DeleteOfficeAssign")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOfficeAssignConfirmed(int id)
        {
            OfficeAssign officeAssign = dbmodel.OfficeAssigns.Find(id);
            dbmodel.OfficeAssigns.Remove(officeAssign);
            dbmodel.SaveChanges();
            return RedirectToAction("manageOfficeAssign");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbmodel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}