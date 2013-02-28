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
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

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

        [Authorize]
        public ActionResult TakeATour()
        {
            return View();
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            var db = new Context();
            ViewBag.States = db.States;
            return View();
        }

        [AllowAnonymous]
        public ActionResult UpdateDistrictJSon(string selectedState)
        {
            var db = new Context();
            var statId = db.States.FirstOrDefault(t => t.ShortForm == selectedState).Id;
            var districts = (from name in db.Districts
                             where name.State_Id == statId
                             select name.DistrictName).ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }


        [AllowAnonymous]
        public ActionResult UpdateSchoolJSon(string selectedDistrict)
        {
            IList<string> schools;
            var db = new Context();
            if (selectedDistrict != "-- Loading Districts --")
            {
                
                var statId = db.Districts.FirstOrDefault(t => t.DistrictName == selectedDistrict).Id;
                schools = (from name in db.Schools
                               where name.District_Id == statId
                               select name.SchoolName).ToList();                
            }
            else
            {
                schools = (from name in db.Schools
                           select name.SchoolName).ToList(); 
            }

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
                    if (db.UserProfiles.Any(t=>t.Email == model.Email))
                    {
                        ModelState.AddModelError("Email","This email address aleady exists. Please try another email");
                        ViewBag.States = db.States;
                        return View(model);
                    }
                    WebSecurity.CreateUserAndAccount( model.UserName, model.Password, new {model.Email, model.First, model.Last});
                    WebSecurity.Login(model.UserName, model.Password);

                    var school = db.Schools.FirstOrDefault(t => t.SchoolName == model.School);
                    var user = db.UserProfiles.FirstOrDefault(t => t.UserName == model.UserName);

                    db.UserProfileSchools.Add(new UserProfileSchool { School = school, UserProfile = user });
                    //db.SaveChanges();
                    return RedirectToAction("TakeATour");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            ViewBag.States = db.States;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Manage

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

        //
        // POST: /UserProfiles/Edit/5

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
        #region Helpers
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
        #endregion
    }
}
