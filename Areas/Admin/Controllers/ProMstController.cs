using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class ProMstController : Controller
    {
        // GET: Admin/ProMst
        JwelleyEntities db = new JwelleyEntities();

        // GET: Admin/ProdMst
        public ActionResult Prod(string search="")
        {
            if(TempData["AddPro"] != null)
            {
                ViewBag.AddProMg = TempData["AddPro"];
            }
            if(TempData["AddProError"] != null)
            {
                ViewBag.AddProErrorMg = TempData["AddProError"];
            }
            if(TempData["EditPro"] != null)
            {
                ViewBag.EditProMg = TempData["EditPro"];
            }
            if(TempData["EditProError"] != null)
            {
                ViewBag.EditProErrorMg = TempData["EditProError"];
            }
            if(TempData["DeletePro"] != null)
            {
                ViewBag.DeleteProMg = TempData["DeletePro"];
            }
            List<ProdMst> prod = db.ProdMsts.Where(row => row.Prod_Type.Contains(search)).ToList();
            return View(prod);
        }

        // GET: Admin/ProdMst/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/ProdMst/Create
        [HttpPost]
        public ActionResult Add(ProdMst e, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.ProdMsts.FirstOrDefault(row => row.Prod_ID == e.Prod_ID);
                if (prod == null)
                {
                    ProdMst stone = new ProdMst();
                    stone.Prod_Type = form["Prod_Type"];
                    db.ProdMsts.Add(e);
                    db.SaveChanges();
                    TempData["AddPro"] = "Add successful.";
                    return RedirectToAction("Prod", "ProMst");
                }
                else
                {
                    TempData["AddProError"] = "Add fail.";
                    return RedirectToAction("Add", "ProMst");
                }
            }
            return View();
        }

        // GET: Admin/ProdMst/Edit/5
        public ActionResult Edit(int id)
        {
            ProdMst pro = db.ProdMsts.Where(row => row.Prod_ID == id).FirstOrDefault();
            return View(pro);
        }

        // POST: Admin/ProdMst/Edit/5
        [HttpPost]
        public ActionResult Edit(ProdMst pr)
        {
            var pro = db.ProdMsts.Where(row => row.Prod_ID == pr.Prod_ID).FirstOrDefault();
            //update
            if (pro != null)
            {
                ProdMst p = db.ProdMsts.Where(rows => rows.Prod_ID == pro.Prod_ID).FirstOrDefault();
                p.Prod_Type = pr.Prod_Type;
                db.SaveChanges();
                TempData["EditPro"] = "Update successful.";
                return RedirectToAction("Prod");
            }
            else
            {
                TempData["EditProError"] = "Update fail.";
                return RedirectToAction("Prod", "ProdMst");
            }
        }

        public ActionResult Delete(int id)
        {
            ProdMst pro = db.ProdMsts.Find(id);
            if (pro != null)
            {
                db.ProdMsts.Remove(pro);
                db.SaveChanges();
                TempData["DeletePro"] = "Delete successful.";
            }
            return RedirectToAction("Prod", "ProMst");
        }

    }
}
