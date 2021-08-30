using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInspection.Models;
using System.Net.Mail;


namespace VehicleInspection.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
                return View("ManagerIndex");

            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact(HomeMessageId? message)
        {
            ViewBag.StatusMessage =
               message == HomeMessageId.SendFeedbackSuccess ? "Your feedback has been sent."
               : "";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = 
                    "<h3>Vehicle Daily Inspection App: Feedback</h3>" +
                    "</br>" +
                    "<p><b>From: </b>{0}</p>" +
                    "<p><b>Reply to: </b>{1}</p>" +
                    "<p>{2}</p>";
                var toAddress = db.Users.FirstOrDefault(u => u.UserName == "admin").Email;
                var email = new MailMessage();
                email.To.Add(toAddress);
                email.Subject = "Feedback submitted";
                email.Body = string.Format(body,model.FromName,model.FromEmail,model.EmailMessage);
                email.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(email);
                    return RedirectToAction("Contact", new { Message = HomeMessageId.SendFeedbackSuccess });
                }
            }
            return View(model);
        }

        public enum HomeMessageId
        {
            SendFeedbackSuccess
        }

    }
}