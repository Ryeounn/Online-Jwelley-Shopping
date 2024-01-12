using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class CategoryAdminController : Controller
    {
        // GET: Admin/CategoryAdmin
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Category(string search = "")
        {
            if (TempData["AddCat"] != null)
            {
                ViewBag.AddCatMg = TempData["AddCat"];
            }
            if (TempData["AddCatError"] != null)
            {
                ViewBag.AddCatErrorMg = TempData["AddCatError"];
            }
            if(TempData["EditCat"] != null)
            {
                ViewBag.EditCarMg = TempData["EditCat"];
            }
            if(TempData["EditCatError"] != null)
            {
                ViewBag.EditCatErrorMg = TempData["EditCatError"];
            }
            if(TempData["DeleteCat"] != null)
            {
                ViewBag.DeleteCatMg = TempData["DeleteCat"];
            }
            List<CatMst> cat = db.CatMsts.Where(row => row.Cat_Name.Contains(search)).ToList();
            return View(cat);

        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string CatName, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                CatName = form["CatName"];
                var catcoder = db.CatMsts.FirstOrDefault(row => row.Cat_Name == CatName);
                if (catcoder == null)
                {
                    CatMst cat = new CatMst();
                    cat.Cat_Name = form["CatName"];
                    db.CatMsts.Add(cat);
                    db.SaveChanges();
                    TempData["AddCat"] = "Add successful.";
                    return RedirectToAction("Category", "CategoryAdmin");
                }
                else
                {
                    TempData["AddCatError"] = "Add fail.";
                    return RedirectToAction("Category", "CategoryAdmin");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            CatMst catcoder = db.CatMsts.Where(row => row.Cat_ID == id).FirstOrDefault();
            return View(catcoder);
        }
        [HttpPost]
        public ActionResult Edit(int id, string CatType, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                id = int.Parse(form["Catid"]);
                CatType = form["Catid"];
                CatMst Catcoder = db.CatMsts.FirstOrDefault(row => row.Cat_ID == id);
                if (Catcoder != null)
                {
                    Catcoder.Cat_Name = form["CatName"];
                    db.SaveChanges();
                    TempData["EditCat"] = "Edit successful.";
                    return RedirectToAction("Category", "CategoryAdmin");
                }
                else
                {
                    TempData["EditCatError"] = "Edit fail.";
                    return RedirectToAction("Category", "CategoryAdmin");
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            var catdl = db.CatMsts.Find(id);
            db.CatMsts.Remove(catdl);
            db.SaveChanges();
            TempData["DeleteCat"] = "Delete successful.";
            return RedirectToAction("Category", "CategoryAdmin");
        }
    }
}