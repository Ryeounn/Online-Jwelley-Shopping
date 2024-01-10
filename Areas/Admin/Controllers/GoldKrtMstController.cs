using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class GoldKrtMstController : Controller
    {
        JwelleyEntities db = new JwelleyEntities();

        // GET: Admin/GoldKrtMst
        public ActionResult Gold(string search = "")
        {
            if (TempData["Add"] != null)
            {
                ViewBag.AddGoldMg = TempData["Add"];
            }
            if (TempData["AddError"] != null)
            {
                ViewBag.AddGoldErrorMg = TempData["AddError"];
            }
            if(TempData["EditGold"] != null)
            {
                ViewBag.EditGoldMg = TempData["EditGold"];
            }
            if(TempData["EditGoldError"] != null)
            {
                ViewBag.EditGoldErrorMg = TempData["EditGoldError"];
            }
            List<GoldKrtMst> prod = db.GoldKrtMsts.Where(x => x.Gold_Crt.Contains(search)).ToList();
            return View(prod);
        }

        // GET: Admin/GoldKrtMst/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/GoldKrtMst/Create
        [HttpPost]
        public ActionResult Add(GoldKrtMst e, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.GoldKrtMsts.FirstOrDefault(row => row.GoldType_ID == e.GoldType_ID);
                if (prod == null)
                {
                    GoldKrtMst stone = new GoldKrtMst();
                    stone.Gold_Crt = form["Gold_Crt"];
                    db.GoldKrtMsts.Add(e);
                    db.SaveChanges();
                    TempData["Add"] = "Add Gold succesful.";
                    return RedirectToAction("Gold", "GoldKrtMst");
                }
                else
                {
                    TempData["AddError"] = "Add fail.";
                    return RedirectToAction("Add", "GoldKrtMst");

                }
            }
            return View();
        }

        // GET: Admin/GoldKrtMst/Edit/5
        public ActionResult Edit(int id)
        {
            GoldKrtMst pro = db.GoldKrtMsts.Where(row => row.GoldType_ID == id).FirstOrDefault();
            return View(pro);

        }

        // POST: Admin/GoldKrtMst/Edit/5
        [HttpPost]
        public ActionResult Edit(GoldKrtMst pro)
        {
            var gold = db.GoldKrtMsts.Where(row => row.GoldType_ID == pro.GoldType_ID).FirstOrDefault();
            //update
            if (gold != null)
            {
                GoldKrtMst st = db.GoldKrtMsts.Where(rows => rows.GoldType_ID == pro.GoldType_ID).FirstOrDefault();
                st.Gold_Crt = pro.Gold_Crt;
                db.SaveChanges();
                TempData["EditGold"] = "Update succesful.";
                return RedirectToAction("Gold", "GoldKrtMst");
            }
            else
            {
                TempData["EditGoldError"] = "Update fail.";
                return RedirectToAction("Gold", "GoldKrtMst");
            }
        }

        // GET: Admin/GoldKrtMst/Delete/5

    }
}