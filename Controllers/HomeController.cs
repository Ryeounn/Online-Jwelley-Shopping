using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Controllers
{
    public class HomeController : Controller 
    {
        // GET: Home
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Index()
        {
            if (TempData["contact"] != null)
            {
                ViewBag.ContactMg = TempData["contact"];
            }
            if (TempData["collection"] != null)
            {
                ViewBag.CollectionMg = TempData["collection"];
            }
            if (TempData["errorr"] != null)
            {
                ViewBag.ErrorrMg = TempData["errorr"];
            }
            if (TempData["news"] != null) 
            {
                ViewBag.NewsMg = TempData["news"];
            }
            var model1 = new Join().SelectHome().ToList();
            var model2 = new Join().SelectHome1().ToList();
            var model3 = new Join().SelectHome2().ToList();
            var model4 = new Join().SelectHome3().ToList();
            var model5 = new Join().SelectHome4().ToList();

            dynamic models1 = new ExpandoObject();
            models1.Home1 = model1;
            models1.Home2 = model2;
            models1.Home3 = model3;
            models1.Home4 = model4;
            models1.Homef = model5;

            return View(models1);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Newss(FormCollection form)
        {
            var mail = form["email"];
            if(ModelState.IsValid)
            {
                News news = new News();
                news.NGmail = mail;
                db.News.Add(news);
                db.SaveChanges();
                TempData["news"] = "Send successful";
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Collection(FormCollection form)
        {
            var mail = form["search2"];
            if(ModelState.IsValid)
            {
                Collection collection = new Collection();
                collection.CGmail = mail;
                db.Collections.Add(collection);
                db.SaveChanges();
                TempData["collection"] = "Send Information successful";
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Policy()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Natural()
        {
            return View();
        }
    }
}