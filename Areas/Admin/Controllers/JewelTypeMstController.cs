using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class JewelTypeMstController : Controller
    {
        // GET: Admin/JewelTypeMst
        JwelleyEntities db = new JwelleyEntities();

        // GET: Admin/JewelTypeMst
        public ActionResult Jewel(string search = "")
        {
            if (TempData["AddJewel"] != null)
            {
                ViewBag.AddJewelMg = TempData["AddJewel"];
            }
            if (TempData["AddJewelError"] != null)
            {
                ViewBag.AddJewelErrorMg = TempData["AddJewelError"];
            }
            if(TempData["updateJewel"] != null)
            {
                ViewBag.UpdateJewel = TempData["updateJewel"];
            }
            if(TempData["updateJewelError"] != null)
            {
                ViewBag.UpdateJewelError = TempData["updateJewelError"];
            }
            List<JewelTypeMst> prod = db.JewelTypeMsts.Where(x => x.Jewellery_Type.Contains(search)).ToList();
            return View(prod);
        }

        // GET: Admin/JewelTypeMst/Details/5

        // GET: Admin/JewelTypeMst/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/JewelTypeMst/Create
        [HttpPost]
        public ActionResult Add(JewelTypeMst e, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.JewelTypeMsts.FirstOrDefault(row => row.ID == e.ID);
                if (prod == null)
                {
                    JewelTypeMst stone = new JewelTypeMst();
                    stone.Jewellery_Type = form["Jewellery_Type"];
                    db.JewelTypeMsts.Add(e);
                    db.SaveChanges();
                    TempData["AddJewel"] = "Add succesful.";
                    return RedirectToAction("Jewel", "JewelTypeMst");
                }
                else
                {
                    TempData["AddJewelError"] = "Add fail.";
                    return RedirectToAction("Create", "JewelTypeMst");

                }
            }
            return View();
        }

        // GET: Admin/JewelTypeMst/Edit/5
        public ActionResult Edit(int id)
        {
            JewelTypeMst pro = db.JewelTypeMsts.Where(row => row.ID == id).FirstOrDefault();
            return View(pro);
        }

        // POST: Admin/JewelTypeMst/Edit/5
        [HttpPost]
        public ActionResult Edit(JewelTypeMst pr)
        {
            var pro = db.JewelTypeMsts.Where(row => row.ID == pr.ID).FirstOrDefault();
            //update
            if (pro != null)
            {
                JewelTypeMst p = db.JewelTypeMsts.Where(rows => rows.ID == pro.ID).FirstOrDefault();
                p.Jewellery_Type = pr.Jewellery_Type;
                db.SaveChanges();
                TempData["updateJewel"] = "Update succesful.";
                return RedirectToAction("Jewel");
            }
            else
            {
                TempData["updateJewelError"] = "Update fail.";
                return RedirectToAction("Jewel", "JewelTypeMst");
            }
        }
    }
}