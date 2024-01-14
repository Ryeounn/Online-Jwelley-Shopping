using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class DimQltySubMstController : Controller
    {
        // GET: Admin/DimQltySubMst
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult DimQltySub(string search = "")
        {
            if (TempData["DimQltySub"] != null)
            {
                ViewBag.AddDimSubMstMg = TempData["DimQltySub"];
            }
            if (TempData["DimQltySubError"] != null)
            {
                ViewBag.AddDimSubMstError = TempData["DimQltySubError"];
            }
            if (TempData["EditDimQltySub"] != null)
            {
                ViewBag.EditDimSubMstMg = TempData["EditDimQltySub"];
            }
            if (TempData["EditDimQltySubError"] != null)
            {
                ViewBag.EditDimSubMstErrorMg = TempData["EditDimQltySubError"];
            }
            if (TempData["DeleteDimMst"] != null)
            {
                ViewBag.DeleteDimSubMstMg = TempData["DeleteDimMst"];
            }
            List<DimQltySubMst> prod = db.DimQltySubMsts.Where(x => x.DimQlty.Contains(search)).ToList();
            return View(prod);

        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DimQltySubMst e, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.DimQltySubMsts.FirstOrDefault(row => row.DimSubType_ID == e.DimSubType_ID);
                if (prod == null)
                {
                    DimQltySubMst min = new DimQltySubMst();
                    min.DimQlty = form["DimQltySub"];
                    min.description = form["Description"];
                    TempData["DimQltySub"] = "Create DimQltySub succesfully!";
                    db.DimQltySubMsts.Add(min);
                    db.SaveChanges();
                    return RedirectToAction("DimQltySub", "DimQltySubMst");
                }
                else
                {
                    TempData["DimQltySubError"] = "Can't Create";
                    return RedirectToAction("DimQltyub", "DimQltySubMst");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            DimQltySubMst pro = db.DimQltySubMsts.Where(row => row.DimSubType_ID == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, String dimQlty, FormCollection form)
        {
            id = int.Parse(form["productid"]);
            dimQlty = form["DimQlty"];
            DimQltySubMst dimQltySubMst = db.DimQltySubMsts.Where(row => row.DimSubType_ID == id).FirstOrDefault();
            if (dimQltySubMst != null)
            {
                dimQltySubMst.DimQlty = form["DimQlty"];
                dimQltySubMst.description = form["description"];
                db.SaveChanges();
                TempData["EditDimQltySub"] = "Edit DimQltySub successfully!";
                return RedirectToAction("DimQltySub", "DimQltySubMst");
            }
            else
            {
                TempData["EditDimQltySubError"] = "Edit DimQltySub fall!";
                return RedirectToAction("DimQltySub", "DimQltySubMst");
            }
        }

        public ActionResult Delete(int id)
        {
            var product = db.DimQltySubMsts.Find(id);
            db.DimQltySubMsts.Remove(product);
            db.SaveChanges();
            TempData["DeleteDimMst"] = "Delete Product successfully!";
            return RedirectToAction("DimQltySub", "DimQltySubMst");
        }
    }
}