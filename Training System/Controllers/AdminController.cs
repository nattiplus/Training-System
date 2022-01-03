using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Training_System.Models;

namespace Training_System.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;
        private DBModels dbmodel;

        public AdminController()
        {
            db = new ApplicationDbContext();
            dbmodel = new DBModels();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/adminProfile
        public ActionResult adminProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = dbmodel.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult adminProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,AspNetUserId,Education")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(admin).State = EntityState.Modified;
                dbmodel.SaveChanges();
                string url = "/adminProfile/" + admin.Id;
                return RedirectToAction(url, "Admin");
            }
            return View(admin);
        }

        // GET: Admin/manageAccount
        public ActionResult manageAccount()
        {
            var users = db.Users.Include(c => c.Roles);
            ViewBag.Users = users.ToList();
            //db.Users.ToList()
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

        // GET: Admin/manageRole
        public ActionResult manageRole()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
                list.Add(new RoleViewModel(role));
            return View(list);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("manageRole");
        }

        public async Task<ActionResult> EditRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("manageRole");
        }

        public async Task<ActionResult> DetailsRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleConfirm(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("manageRole");
        }

        // GET: /Admin/manageAdmin
        public ActionResult manageAdmin()
        {
            var admin = dbmodel.Admins.Include(a => a.AspNetUser);
            return View(admin.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult DetailsAdminProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = dbmodel.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult AssignAdminProfile()
        {
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignAdminProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,Education,AspNetUserId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Id = admin.AspNetUserId;
                dbmodel.Admins.Add(admin);
                dbmodel.SaveChanges();
                return RedirectToAction("manageAdmin");
            }

            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", admin.AspNetUserId);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult EditAdminProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = dbmodel.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", admin.AspNetUserId);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,Education,AspNetUserId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(admin).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageAdmin");
            }
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", admin.AspNetUserId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult DeleteAdminProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = dbmodel.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("DeleteAdminProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminProfileConfirmed(string id)
        {
            Admin admin = dbmodel.Admins.Find(id);
            dbmodel.Admins.Remove(admin);
            dbmodel.SaveChanges();
            return RedirectToAction("manageAdmin");
        }

        // GET: /Admin/manageTrainingStaff
        public ActionResult manageTrainingStaff()
        {
            var trainingStaffs = dbmodel.TrainingStaffs.Include(t => t.AspNetUser);
            return View(trainingStaffs.ToList());
        }

        // GET: TrainingStaffs/Details/5
        public ActionResult DetailsStaffProfile(string id)
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

        // GET: TrainingStaffs/Create
        public ActionResult AssignStaffProfile()
        {
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: TrainingStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignStaffProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,Education,AspNetUserId")] TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                trainingStaff.Id = trainingStaff.AspNetUserId;
                dbmodel.TrainingStaffs.Add(trainingStaff);
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainingStaff");
            }

            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainingStaff.AspNetUserId);
            return View(trainingStaff);
        }

        // GET: TrainingStaffs/Edit/5
        public ActionResult EditStaffProfile(string id)
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
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainingStaff.AspNetUserId);
            return View(trainingStaff);
        }

        // POST: TrainingStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStaffProfile([Bind(Include = "Id,FullName,Age,DoB,Email,Telephone,Location,Department,ExperienceDetails,Type,WorkingPlace,Education,AspNetUserId")] TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                dbmodel.Entry(trainingStaff).State = EntityState.Modified;
                dbmodel.SaveChanges();
                return RedirectToAction("manageTrainingStaff");
            }
            ViewBag.AspNetUserId = new SelectList(dbmodel.AspNetUsers, "Id", "UserName", trainingStaff.AspNetUserId);
            return View(trainingStaff);
        }

        // GET: TrainingStaffs/Delete/5
        public ActionResult DeleteStaffProfile(string id)
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

        // POST: TrainingStaffs/Delete/5
        [HttpPost, ActionName("DeleteStaffProfile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStaffProfileConfirmed(string id)
        {
            TrainingStaff trainingStaff = dbmodel.TrainingStaffs.Find(id);
            dbmodel.TrainingStaffs.Remove(trainingStaff);
            dbmodel.SaveChanges();
            return RedirectToAction("manageTrainingStaff");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbmodel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}