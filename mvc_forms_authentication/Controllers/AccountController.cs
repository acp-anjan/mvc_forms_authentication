using mvc_forms_authentication.Helper;
using mvc_forms_authentication.Models;
using mvc_forms_authentication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace mvc_forms_authentication.Controllers
{
    public class AccountController : Controller
    {
        private FormAuthenticationContext dbContext;

        public AccountController()
        {
            dbContext = new FormAuthenticationContext();
        }

        [HttpGet]
        public ActionResult Register()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordSalt = CryptoService.GenerateSalt();
                byte[] bytePassword = Encoding.UTF8.GetBytes(model.UserPassword);
                var hmac = CryptoService.ComputeHMAC256(bytePassword, passwordSalt);

                var verificationCode = Guid.NewGuid().ToString();

                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    UserName = model.UserName,
                    Email = model.UserEmail,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    UserPassword = Convert.ToBase64String(hmac),
                    VerificationCode = verificationCode,
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                var activationUrl = "/Account/ActivateAccount?VerificationCode=" + verificationCode;
                var activationLink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, activationUrl);

                RegisterEmailHelper.SendVerificationLink(model.UserEmail, activationLink);

                return View();
                
            }
            return View();
        }

        public ActionResult UserNameExists(string userName)
        {
            bool isUsernameExists = dbContext.Users.Any(m => m.UserName == userName);
            return Json(!isUsernameExists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailExists(string userEmail)
        {
            bool isEmailExists = dbContext.Users.Any(m => m.Email == userEmail);
            return Json(!isEmailExists, JsonRequestBehavior.AllowGet);
        }
    }
}