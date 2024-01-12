using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class CartListController : Controller
    {
        // GET: Admin/CartList
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult CartList(string oc = "")
        {
            if (TempData["Editcl"] != null)
            {
                ViewBag.EditMg = TempData["Editcl"];
            }
            if (TempData["ErrorEditcl"] != null)
            {
                ViewBag.ErrorEditMg = TempData["ErrorEditcl"];
            }
            List<CartList> cartLists = db.CartLists.Where(x => x.OrderCode.Contains(oc)).ToList();
            return View(cartLists);
        }
        public ActionResult Edit(int id)
        {
            CartList cartmst = db.CartLists.Where(row => row.ID == id).FirstOrDefault();
            return View(cartmst);
        }
        [HttpPost]
        public ActionResult Edit(CartList ct)
        {
            CartList cart = db.CartLists.Where(row => row.ID == ct.ID).FirstOrDefault();
            if (cart != null)
            {
                cart.Status = ct.Status;
                db.SaveChanges();
                TempData["Editcl"] = "Edit Successfully !!";
                return RedirectToAction("CartList", "CartList");
            }
            else
            {
                TempData["ErrorEditcl"] = "Edit fail !!";
                return View("EditProduct");
            }
        }
    }
    }