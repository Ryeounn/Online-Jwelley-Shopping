using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class ItemMstController : Controller
    {
        // GET: Admin/ItemMst
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Item(String sc = "")
        {
            if (TempData["AddItemMst"] != null)
            {
                ViewBag.AddDimMstMg = TempData["AddItemMst"];
            }
            if (TempData["EditDimInfoMstError"] != null)
            {
                ViewBag.AddDimMstError = TempData["EditDimInfoMstError"];
            }
            if (TempData["EditItemMst"] != null)
            {
                ViewBag.EditDimInfoMstMg = TempData["EditItemMst"];
            }
            if (TempData["EditItemMstError"] != null)
            {
                ViewBag.EditDimInfoMstErrorMg = TempData["EditItemMstError"];
            }
            if (TempData["DeleteItemMst"] != null)
            {
                ViewBag.DeleteDimMstMg = TempData["DeleteItemMst"];
            }
            List<ItemMst> item = db.ItemMsts.Where(x => x.Prod_Quality.Contains(sc)).ToList();
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ItemMst item, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var prod = db.ItemMsts.FirstOrDefault(row => row.Style_Code == item.Style_Code);
                if (prod == null)
                {
                    ItemMst itemMst = new ItemMst();
                    itemMst.Pairs = decimal.Parse(form["Pairs"]);
                    itemMst.Quantity = decimal.Parse(form["Quantity"]);
                    itemMst.Prod_Quality = form["Prod_Quality"];
                    itemMst.Gold_Wt = decimal.Parse(form["Gold_Wt"]);
                    itemMst.Stone_Wt = decimal.Parse(form["Stone_Wt"]);
                    itemMst.Wstg_Per = decimal.Parse(form["Wstg_Per"]);
                    itemMst.Wstg = decimal.Parse(form["Wstg"]);
                    itemMst.Tot_Gross_Wt = decimal.Parse(form["Tot_Gross_Wt"]);
                    itemMst.Gold_Rate = decimal.Parse(form["Gold_Rate"]);
                    itemMst.Gold_Amt = decimal.Parse(form["Gold_Amt"]);
                    itemMst.Gold_Making = decimal.Parse(form["Gold_Making"]);
                    itemMst.Stone_Making = decimal.Parse(form["Gold_Rate"]);
                    itemMst.Other_Making = decimal.Parse(form["Other_Making"]);
                    itemMst.Tot_Making = decimal.Parse(form["Tot_Making"]);
                    itemMst.MRP = decimal.Parse(form["MRP"]);
                    db.ItemMsts.Add(itemMst);
                    db.SaveChanges();
                    TempData["AddItemMst"] = "Add succesful.";
                    return RedirectToAction("Item", "ItemMst");
                }
                else
                {
                    TempData["AddItemMstError"] = "Add fail.";
                    return RedirectToAction("Item", "ItemMst");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ItemMst pro = db.ItemMsts.Where(row => row.Style_Code == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            id = int.Parse(form["productid"]);

            ItemMst itemMst = db.ItemMsts.Where(row => row.Style_Code == id).FirstOrDefault();
            if (itemMst != null)
            {
                itemMst.Pairs = decimal.Parse(form["Pairs"]);
                itemMst.Quantity = decimal.Parse(form["Quantity"]);
                itemMst.Prod_Quality = form["Prod_Quality"];
                itemMst.Gold_Wt = decimal.Parse(form["Gold_Wt"]);
                itemMst.Stone_Wt = decimal.Parse(form["Stone_Wt"]);
                itemMst.Wstg_Per = decimal.Parse(form["Wstg_Per"]);
                itemMst.Wstg = decimal.Parse(form["Wstg"]);
                itemMst.Tot_Gross_Wt = decimal.Parse(form["Tot_Gross_Wt"]);
                itemMst.Gold_Rate = decimal.Parse(form["Gold_Rate"]);
                itemMst.Gold_Amt = decimal.Parse(form["Gold_Amt"]);
                itemMst.Gold_Making = decimal.Parse(form["Gold_Making"]);
                itemMst.Stone_Making = decimal.Parse(form["Gold_Rate"]);
                itemMst.Other_Making = decimal.Parse(form["Other_Making"]);
                itemMst.Tot_Making = decimal.Parse(form["Tot_Making"]);
                itemMst.MRP = decimal.Parse(form["MRP"]);
                db.SaveChanges();
                TempData["EditItemMst"] = "Update succesful.";
                return RedirectToAction("Item", "ItemMst");
            }
            else
            {
                TempData["EditItemMstError"] = "Update fail.";
                return RedirectToAction("Item", "ItemMst");
            }
        }
        public ActionResult Delete(int id)
        {
            var maianh = db.ItemMsts.Find(id);
            db.ItemMsts.Remove(maianh);
            db.SaveChanges();
            TempData["DeleteItemMst"] = "Delete successful.";
            return RedirectToAction("Item", "ItemMst");
        }
    }
}