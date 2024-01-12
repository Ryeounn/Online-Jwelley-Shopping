using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class BrandAdminController : Controller
    {
        // GET: Admin/BrandAdmin
        // GET: Admin/BrandAdmin
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Brand(string search = "")
        {
            if (TempData["AddBrand"] != null)
            {
                ViewBag.AddBrandMg = TempData["AddBrand"];
            }
            if (TempData["AddBrandError"] != null)
            {
                ViewBag.AddBrandErrorMg = TempData["AddBrandError"];
            }
            if (TempData["EditBrand"] != null)
            {
                ViewBag.EditBrandMg = TempData["EditBrand"];
            }
            if (TempData["EditBrandError"] != null)
            {
                ViewBag.EditBrandErrorMg = TempData["EditBrandError"];
            }
            if (TempData["DeleteBrand"] != null)
            {
                ViewBag.DeleteBrand = TempData["DeleteBrand"];
            }
            List<BrandMst> brands = db.BrandMsts.Where(row => row.Brand_Type.Contains(search)).ToList();
            return View(brands);

        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string BrandType, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                BrandType = form["BrandType"];
                var brandcoder = db.BrandMsts.FirstOrDefault(row => row.Brand_Type == BrandType);
                if (brandcoder == null)
                {
                    BrandMst brand = new BrandMst();
                    brand.Brand_Type = form["BrandType"];
                    db.BrandMsts.Add(brand);
                    db.SaveChanges();
                    TempData["AddBrand"] = "Add successful.";
                    return RedirectToAction("Brand", "BrandAdmin");
                }
                else
                {
                    TempData["AddBrandError"] = "Add fail.";
                    return RedirectToAction("Brand", "BrandAdmin");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            BrandMst brandcoder = db.BrandMsts.Where(row => row.Brand_ID == id).FirstOrDefault();
            return View(brandcoder);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(form["Brandid"]);
                var BrandType = form["BrandType"];
                BrandMst brandcoder = db.BrandMsts.FirstOrDefault(row => row.Brand_ID == id);
                if (brandcoder != null)
                {
                    brandcoder.Brand_Type = form["BrandType"];
                    db.SaveChanges();
                    TempData["EditBrand"] = "Update successful.";
                    return RedirectToAction("Brand", "BrandAdmin");
                }
                else
                {
                    TempData["EditBrandError"] = "Update fail.";
                    return RedirectToAction("Brand", "BrandAdmin");
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            var brandse = db.BrandMsts.Find(id);
            db.BrandMsts.Remove(brandse);
            db.SaveChanges();
            TempData["DeleteBrand"] = "Delete successful.";
            return RedirectToAction("Brand", "BrandAdmin");
        }
    }
}