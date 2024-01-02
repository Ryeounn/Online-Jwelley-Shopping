using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
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
                TempData["result"] = "Send Information successful";
                return RedirectToAction("Index");
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