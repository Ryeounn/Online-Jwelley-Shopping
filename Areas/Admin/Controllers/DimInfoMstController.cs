using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class DimInfoMstController : Controller
    {
        // GET: Admin/DimInfo
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult DimInfo()
        {
            if (TempData["AddDimInfoMst"] != null)
            {
                ViewBag.AddDimMstMg = TempData["AddDimInfoMst"];
            }
            if (TempData["EditDimInfoMstError"] != null)
            {
                ViewBag.AddDimMstError = TempData["EditDimInfoMstError"];
            }
            if (TempData["EditDimInfoMst"] != null)
            {
                ViewBag.EditDimInfoMstMg = TempData["EditDimInfoMst"];
            }
            if (TempData["EditDimInfoMstError"] != null)
            {
                ViewBag.EditDimInfoMstErrorMg = TempData["EditDimInfoMstError"];
            }
            if (TempData["DeleteDimInfoMst"] != null)
            {
                ViewBag.DeleteDimMstMg = TempData["DeleteDimInfoMst"];
            }
            List<DimInfoMst> mai = db.DimInfoMsts.ToList();
            return View(mai);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(DimInfoMst nin, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.DimInfoMsts.FirstOrDefault(row => row.DimID == nin.DimID);
                if (prod == null)
                {
                    DimInfoMst min = new DimInfoMst();
                    min.DimType = form["DimType"];
                    min.DimSubType = form["DimSubType"];
                    min.DimCrt = form["DimCrt"];
                    min.DimPrice = decimal.Parse(form["DimPrice"]);
                    min.DimImg = form["DimImg"];
                    db.DimInfoMsts.Add(min);
                    db.SaveChanges();
                    TempData["AddDimInfoMst"] = "Add succesful.";
                    return RedirectToAction("DimInfo", "DimInfoMst");
                }
                else
                {
                    TempData["AddDimInfoMstError"] = "Add fail.";
                    return RedirectToAction("DimInfo", "DimInfoMst");
                }
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            DimInfoMst pro = db.DimInfoMsts.Where(row => row.DimID == id).FirstOrDefault();
            return View(pro);

        }
        [HttpPost]
        public ActionResult Edit(int id, String dimTyoe, FormCollection form)
        {
            id = int.Parse(form["DimID"]);
            dimTyoe = form["DimType"];
            DimInfoMst min = db.DimInfoMsts.Where(row => row.DimID == id).FirstOrDefault();
            if (min != null)
            {
                min.DimType = form["DimType"];
                min.DimSubType = form["DimSubType"];
                min.DimCrt = form["DimCrt"];
                min.DimPrice = decimal.Parse(form["DimPrice"]);
                min.DimImg = form["DimImg"];
                db.SaveChanges();
                TempData["EditDimInfoMst"] = "Update succesful.";
                return RedirectToAction("DimInfo", "DimInfoMst");
            }
            else
            {
                TempData["EditDimInfoMstError"] = "Update fail.";
                return RedirectToAction("DimInfo", "DimInfoMst");
            }
        }
        public ActionResult Delete(int id)
        {
            var maianh = db.DimInfoMsts.Find(id);
            db.DimInfoMsts.Remove(maianh);
            db.SaveChanges();
            TempData["DeleteDimInfoMst"] = "Delete successful.";
            return RedirectToAction("DimInfo", "DimInfoMst");
        }

    }
}