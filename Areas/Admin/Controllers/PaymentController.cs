using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class PaymentController : Controller
    {
       
            JwelleyEntities db = new JwelleyEntities();
            public ActionResult Payment(string search = "")
            {
                if (TempData["Addp"] != null)
                {
                    ViewBag.Addp = TempData["Addp"];
                }
                if (TempData["errorAd"] != null)
                {
                    ViewBag.ErrorAp = TempData["errorAd"];
                }
                if (TempData["EditP"] != null)
                {
                    ViewBag.Editp = TempData["EditP"];
                }
                if (TempData["errorP"] != null)
                {
                    ViewBag.ErrorEdit = TempData["errorP"];
                }
                if (TempData["DeleteP"] != null)
                {
                    ViewBag.DeleteP = TempData["DeleteP"];
                }
                if (TempData["errorlP"] != null)
                {
                    ViewBag.ErrorDeleteP = TempData["errorlP"];
                }
                List<Payment> payment = db.Payments.Where(x => x.type.Contains(search)).ToList();
                return View(payment);
            }
            public ActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Create(Payment payment)
            {
                if (ModelState.IsValid)
                {
                    db.Payments.Add(payment);
                    db.SaveChanges();
                    TempData["Addp"] = "Add Payment Successfully!";
                    return RedirectToAction("Payment", "Payment");

                }
                else
                {
                    TempData["errorAd"] = "Error Add!!";
                    return View();
                }
            }
            public ActionResult Edit(int id)
            {
                Payment payment = db.Payments.Where(row => row.ID == id).FirstOrDefault();
                return View(payment);
            }
            [HttpPost]
            public ActionResult Edit(Payment pay)
            {
                Payment payment = db.Payments.Where(row => row.ID == pay.ID).FirstOrDefault();
                if (payment != null)
                {
                    payment.type = pay.type;
                    payment.numbercard = pay.numbercard;
                    payment.cgv = pay.cgv;
                    payment.expiration_date = pay.expiration_date;
                    db.SaveChanges();
                    TempData["EditP"] = "Edit Product successfully!";

                    return RedirectToAction("Payment", "Payment");
                }
                else
                {
                    TempData["errorP"] = "Error Edit!!";
                    return View();
                }
            }

            public ActionResult Delete(int ID)
            {
                var payment = db.Payments.Where(x => x.ID == ID).FirstOrDefault();
                if (payment != null)
                {
                    db.Payments.Remove(payment);
                    db.SaveChanges();
                    TempData["DeleteP"] = "Delete Product successfully!";
                    return RedirectToAction("Payment", "Payment");
                }
                else
                {
                    TempData["errorlP"] = "Error Delete!!";
                    return View();

                }

            }

        }
    }

