using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class DimMstController : Controller
    {
            // GET: Admin/DimMst
            JwelleyEntities db = new JwelleyEntities();
            public ActionResult Dim()
            {
                if (TempData["AddDimMst"] != null)
                {
                    ViewBag.AddDimMstMg = TempData["AddDimMst"];
                }
                if (TempData["AddDimMstError"] != null)
                {
                    ViewBag.AddDimMstError = TempData["AddDimMstError"];
                }
                if(TempData["EditDimMst"] != null)
                {
                    ViewBag.EditDimMstMg = TempData["EditDimMst"];
                }
                if(TempData["EditDimMstError"] != null)
                {
                    ViewBag.EditDimMstErrorMg = TempData["EditDimMstError"];
                }
                if(TempData["DeleteDimMst"] != null)
                {
                    ViewBag.DeleteDimMstMg = TempData["DeleteDimMst"];
                }
                List<DimMst> products = db.DimMsts.ToList();
                return View(products);
            }
            public ActionResult Add()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Add(DimMst e, FormCollection form)
            {
                if (ModelState.IsValid)
                {
                    var prod = db.DimMsts.FirstOrDefault(row => row.DimMst_ID == e.DimMst_ID);
                    if (prod == null)
                    {
                        DimMst min = new DimMst();
                        min.Dim_Crt = decimal.Parse(form["Dim_Crt"]);
                        min.Dim_Pcs = decimal.Parse(form["Dim_Pcs"]);
                        min.Dim_Gm = decimal.Parse(form["Dim_Gm"]);
                        min.Dim_Size = decimal.Parse(form["Dim_Size"]);
                        min.Dim_Rate = decimal.Parse(form["Dim_Rate"]);
                        min.Dim_Amt = decimal.Parse(form["Dim_Amt"]);
                        db.DimMsts.Add(min);
                        db.SaveChanges();
                        TempData["AddDimMst"] = "Add succesful.";
                        return RedirectToAction("Dim", "DimMst");
                    }
                    else
                    {
                        TempData["AddDimMstError"] = "Add fail.";
                        return RedirectToAction("Dim", "DimMst");
                    }
                }
                return View();
            }
            public ActionResult Edit(int id)
            {
                DimMst pro = db.DimMsts.Where(row => row.DimMst_ID == id).FirstOrDefault();
                return View(pro);
            }
            [HttpPost]
            public ActionResult Edit(int id, String dim_Crt, FormCollection form)
            {
                id = int.Parse(form["productid"]);
                dim_Crt = form["Dim_Crt"];
                DimMst min = db.DimMsts.Where(row => row.DimMst_ID == id).FirstOrDefault();
                if (min != null)
                {
                    min.Dim_Crt = decimal.Parse(form["Dim_Crt"]);
                    min.Dim_Pcs = decimal.Parse(form["Dim_Pcs"]);
                    min.Dim_Gm = decimal.Parse(form["Dim_Gm"]);
                    min.Dim_Size = decimal.Parse(form["Dim_Size"]);
                    min.Dim_Rate = decimal.Parse(form["Dim_Rate"]);
                    min.Dim_Amt = decimal.Parse(form["Dim_Amt"]);
                    db.SaveChanges();
                    TempData["EditDimMst"] = "Update succesful.";
                    return RedirectToAction("Dim", "DimMst");
                }
                else
                {
                    TempData["EditDimMstError"] = "Update fail.";
                    return RedirectToAction("Dim", "DimMst");
                }
            }
            public ActionResult Delete(int id)
            {
                var product = db.DimMsts.Find(id);
                db.DimMsts.Remove(product);
                db.SaveChanges();
                TempData["DeleteDimMst"] = "Delete successful.";
                return RedirectToAction("Dim", "DimMst");
            }
    }
}