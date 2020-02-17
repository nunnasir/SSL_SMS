using SSL_SMS.BLL;
using SSL_SMS.DAL;
using SSL_SMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SSL_SMS.Controllers
{
    public class AccountController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();
        AccountBLL _accountbll = new AccountBLL();

        public ActionResult Login()
        {
            LoginVm model = new LoginVm();
            model.ErrorMessage = "";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm model)
        {
            model.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                var password = _accountbll.encrypt(model.User_Password);
                TbleUser user = db.TbleUsers.FirstOrDefault(g => g.User_Name == model.User_Name && g.User_Password == password);

                if(user == null)
                {
                    model.ErrorMessage = "Username or Password are incorrect!";
                    return View(model);
                }else if (user != null && user.Confirm_Email == 0)
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.User_Email });
                }

                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountVm model)
        {
            if (ModelState.IsValid)
            {
                TbleUser user = new TbleUser();
                user.User_Name = model.User_Name;
                user.User_Email = model.User_Email;
                user.Confirm_Email = 0;
                user.User_Password = _accountbll.encrypt(model.User_Password);

                db.TbleUsers.Add(user);

                var result = db.SaveChanges();

                if(result == 1)
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    string MailName = "nasir@subrasystems.com";
                    string Password = "abc123DE45";

                    msg.From = new MailAddress(MailName);
                    msg.To.Add(user.User_Email);
                    msg.Subject = "Email confirmation";
                    msg.Body = string.Format("Dear {0}, {1} Thank you for your registration, please click on the below link to complete your registration: " +
                        "<a href =\"{2}\">User Email Confirm</a>", user.User_Name, Environment.NewLine, Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.User_Email }, Request.Url.Scheme));

                    msg.IsBodyHtml = true;
                    smtp.Credentials = new NetworkCredential(MailName, Password);
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.Send(msg);

                    return RedirectToAction("Confirm", "Account", new { Email = user.User_Email });
                }

            }

            return View();
        }

        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }

        public ActionResult ConfirmEmail(string Token, string Email)
        {
            int id = int.Parse(Token);
            var user = db.TbleUsers.FirstOrDefault(m => m.Id == id);
            if(user != null)
            {
                if(user.User_Email == Email)
                {
                    user.Confirm_Email = 1;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
            }

            return RedirectToAction("Create");
        }

    }
}