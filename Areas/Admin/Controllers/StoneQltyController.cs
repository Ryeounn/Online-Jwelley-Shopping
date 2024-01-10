using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class StoneQltyController : Controller
    {
        JwelleyEntities db = new JwelleyEntities();
        // GET: Admin/StoneQltyMst
        public ActionResult Stoneqlt(string search = "")
        {
            if (TempData["AddStoneQlty"] != null)
            {
                ViewBag.AddStoneQltyMg = TempData["AddStoneQlty"];
            }
            if (TempData["AddStoneQltyError"] != null)
            {
                ViewBag.AddStoneQltyErrorMg = TempData["AddStoneQltyError"];
            }
            if(TempData["EditStoneQlty"] != null)
            {
                ViewBag.EditStoneQltyMg = TempData["EditStoneQlty"];
            }
            if(TempData["EditStoneQltyError"] != null)
            {
                ViewBag.EditStoneQltyErrorMg = TempData["EditStoneQltyError"];
            }
            List<StoneQltyMst> prod = db.StoneQltyMsts.Where(x => x.StoneQlty.Contains(search)).ToList();
            return View(prod);

        }

        // GET: Admin/StoneQltyMst/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/StoneQltyMst/Create
        [HttpPost]
        public ActionResult Add(StoneQltyMst q, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var qltyMst = db.StoneQltyMsts.FirstOrDefault(row => row.StoneQlty_ID == q.StoneQlty_ID);
                if (qltyMst == null)
                {
                    StoneQltyMst stoneqlty = new StoneQltyMst();
                    stoneqlty.StoneQlty = form["StoneQlty"];
                    db.StoneQltyMsts.Add(q);
                    db.SaveChanges();
                    TempData["AddStoneQlty"] = "Create StoneQltyMst succesfully!";
                    return RedirectToAction("Stoneqlt", "StoneQlty");
                }
                else
                {
                    TempData["AddStoneQltyError"] = "Can't Create";
                    return RedirectToAction("Add", "StoneQlty");

                }
            }
            return View();
        }

        // GET: Admin/StoneQltyMst/Edit/5
        public ActionResult Edit(int id)
        {
            StoneQltyMst pro = db.StoneQltyMsts.Where(row => row.StoneQlty_ID == id).FirstOrDefault();
            return View(pro);
        }

        // POST: Admin/ProdMst/Edit/5
        [HttpPost]
        public ActionResult Edit(StoneQltyMst pr)
        {
            var pro = db.StoneQltyMsts.Where(row => row.StoneQlty_ID == pr.StoneQlty_ID).FirstOrDefault();
            //update
            if (pro != null)
            {
                StoneQltyMst p = db.StoneQltyMsts.Where(rows => rows.StoneQlty_ID == pro.StoneQlty_ID).FirstOrDefault();
                p.StoneQlty = pr.StoneQlty;
                db.SaveChanges();
                TempData["EditStoneQlty"] = "Edit succesful.";
                return RedirectToAction("Stoneqlt", "StoneQlty");
            }
            else
            {
                TempData["EditStoneQltyError"] = "Edit fail.";
                return RedirectToAction("Stoneqlt", "StoneQlty");
            }
        }
    }
}