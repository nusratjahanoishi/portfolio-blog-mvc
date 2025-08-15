/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebsite.Controllers
{
    public class ContactController
    {
    }
}
*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Contact Me";
            return View(new ContactMessage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                // Store in session
                var messages = Session["ContactMessages"] as List<ContactMessage> ?? new List<ContactMessage>();
                model.Id = new Random().Next(1000, 9999);
                model.CreatedDate = DateTime.Now;
                messages.Add(model);
                Session["ContactMessages"] = messages;

                TempData["Success"] = "Thank you! Your message has been sent successfully. I'll get back to you soon!";
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Contact Me";
            return View(model);
        }

        public ActionResult Messages()
        {
            var messages = Session["ContactMessages"] as List<ContactMessage> ?? new List<ContactMessage>();
            ViewBag.Title = "Contact Messages";
            return View(messages);
        }
    }
}