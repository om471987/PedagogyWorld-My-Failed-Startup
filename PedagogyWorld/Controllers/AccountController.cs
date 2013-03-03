using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using PedagogyWorld.Models;

using IsolationLevel = System.Transactions.IsolationLevel;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(GenerateRegisterForm());
        }

        [AllowAnonymous]
        public ActionResult UpdateDistrictJSon(int stateId)
        {
            var db = new Context();
            var districts = (from name in db.Districts
                             where name.State_Id == stateId
                             select new { name.Id, name.DistrictName }).AsEnumerable();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult UpdateSchoolJSon(int districtId)
        {
            var db = new Context();
            districtId = districtId == -1 ? 1 : districtId;
            var schools = (from name in db.Schools
            where name.District_Id == districtId
            select new { name.Id, name.SchoolName }).AsEnumerable();
            return Json(schools, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            var db = new Context();
            if (ModelState.IsValid)
            {
                try
                {
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { model.Email, model.First, model.Last });
                    //WebSecurity.Login(model.UserName, model.Password);

                    var school = db.Schools.FirstOrDefault(t => t.Id == model.School);
                    var user = db.UserProfiles.FirstOrDefault(t => t.UserName == model.UserName);
                    db.UserProfileSchools.Add(new UserProfileSchool { School = school, UserProfile = user });

                    foreach (var gradeId in model.GradeIds)
	                {
                        var userGrade = new UserGrade { UserProfile = user, Grade_Id = gradeId };
                        db.UserGrades.Add(userGrade);
	                }

                    foreach (var subjectId in model.SubjectIds)
                    {
                        var userSubject = new UserSubject { UserProfile = user, Subject_Id = subjectId };
                        db.UserSubjects.Add(userSubject);
                    }
                    //db.SaveChanges();

                    WebSecurity.Login("omkar", "abc123");
                    return RedirectToAction("TakeATour", "Home", new { Area = "" });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(GenerateRegisterForm());
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DoesUserNameExist(string userName)
        {
            var db = new Context();
            var result = db.UserProfiles.Any(t => t.UserName == userName);
            return Json(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DoesEmailExist(string email)
        {
            var db = new Context();
            var result = db.UserProfiles.Any(t => t.Email == email);
            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToAction("Index", "Unit", new {Area=""});
                //return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }            

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public ActionResult Edit()
        {
            var context = new Context();
            var id = context.UserProfiles.FirstOrDefault(t => t.UserName == User.Identity.Name).UserId;
            var userprofile = context.UserProfiles.Single(x => x.UserId == id);
            return View(userprofile);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserProfile userprofile)
        {
            var context = new Context();
            if (ModelState.IsValid)
            {
                context.Entry(userprofile).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index", "Unit");
            }
            return View(userprofile);
        }
        
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        private RegisterModel GenerateRegisterForm()
        {
            var db = new Context();
            var model = new RegisterModel();
            model.States = db.States;
            
            var result = new List<SelectListItem>();
            foreach (var t in db.Subjects)
            {
                result.Add(new SelectListItem
                {
                    Text = t.SubjectName,
                    Value = t.Id.ToString()
                });
            }
            model.Subjects = result.ToList();

            result = new List<SelectListItem>();
            foreach (var t in db.Grades)
            {
                result.Add(new SelectListItem
                {
                    Text = t.GradeName,
                    Value = t.Id.ToString()
                });
            }
            model.Grades = result.ToList();
            return model;
        }
    }
}
