using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class DimQltyMstController : Controller
    {
        JwelleyEntities db = new JwelleyEntities();


        public ActionResult DimQlty(string search = "")
        {
            if (TempData["AddDimQltyMst"] != null)
            {
                ViewBag.AddDimQltyMstMg = TempData["AddDimQltyMst"];
            }
            if (TempData["AddDimQltyMstError"] != null)
            {
                ViewBag.AddDimQltyMstError = TempData["AddDimQltyMstError"];
            }
            if (TempData["EditDimQltyMst"] != null)
            {
                ViewBag.EditDimQltyMstMg = TempData["EditDimQltyMst"];
            }
            if (TempData["EditDimQltyMstError"] != null)
            {
                ViewBag.EditDimQltyMstErrorMg = TempData["EditDimQltyMstError"];
            }
            if (TempData["DeleteDimQltyMst"] != null)
            {
                ViewBag.DeleteDimMstMg = TempData["DeleteDimQltyMst"];
            }
            List<DimQltyMst> prod = db.DimQltyMsts.Where(x => x.DimQlty.Contains(search)).ToList();
            return View(prod);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(DimQltyMst e, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.DimQltyMsts.FirstOrDefault(row => row.DimQlty_ID == e.DimQlty_ID);
                if (prod == null)
                {
                    DimQltyMst min = new DimQltyMst();
                    min.DimQlty = form["DimQlty"];
                    TempData["AddDimQltyMst"] = "Create DimQlty succesfully!";
                    db.DimQltyMsts.Add(e);
                    db.SaveChanges();
                    return RedirectToAction("DimQlty", "DimQltyMst");
                }
                else
                {
                    TempData["AddDimQltyMstError"] = "Can't Create";
                    return RedirectToAction("DimQlty", "DimQltyMst");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            DimQltyMst pro = db.DimQltyMsts.Where(row => row.DimQlty_ID == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, String DimQlity, FormCollection form)
        {
            id = int.Parse(form["productid"]);
            DimQlity = form["DimQlty"];
            DimQltyMst gold = db.DimQltyMsts.Where(row => row.DimQlty_ID == id).FirstOrDefault();
            //update
            if (gold != null)
            {
                gold.DimQlty = form["DimQlty"];
                db.SaveChanges();
                TempData["EditDimQltyMst"] = "Edit DimQlty succesfully!";
                return RedirectToAction("DimQlty", "DimQltyMst");
            }
            else
            {
                TempData["EditDimQltyMstError"] = "Can't update";
                return RedirectToAction("DimQlty", "DimQltyMst");
            }
        }

        public ActionResult Delete(int id)
        {
            var s = db.DimQltyMsts.Find(id);
            db.DimQltyMsts.Remove(s);
            db.SaveChanges();
            TempData["DeleteDimQltyMst"] = "Delete DimQltyMst successfully!";
            return RedirectToAction("DimQlty", "DimQltyMst");
        }
    }
}