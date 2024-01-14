using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class CertifyAdminController : Controller
    {
        // GET: Admin/CertifyAdmin
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Certify(string search = "")
        {
            if (TempData["AddCertify"] != null)
            {
                ViewBag.AddCertifyMg = TempData["AddCertify"];
            }
            if (TempData["AddCertifyError"] != null)
            {
                ViewBag.AddCertifyErrorMg = TempData["AddCertifyError"];
            }
            if (TempData["EditCertify"] != null)
            {
                ViewBag.EditCertifyMg = TempData["EditCertify"];
            }
            if (TempData["EditCertifyError"] != null)
            {
                ViewBag.EditCertifyErrorMg = TempData["EditCertifyError"];
            }
            if (TempData["DeleteCertify"] != null)
            {
                ViewBag.DeleteCertify = TempData["DeleteCertify"];
            }
            List<CertifyMst> certifys = db.CertifyMsts.Where(row => row.Certify_Type.Contains(search)).ToList();
            return View(certifys);

        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string CertifyType, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                CertifyType = form["CertifyType"];
                var certifycoder = db.CertifyMsts.FirstOrDefault(row => row.Certify_Type == CertifyType);
                if (certifycoder == null)
                {
                    CertifyMst certify = new CertifyMst();
                    certify.Certify_Type = form["CertifyType"];
                    db.CertifyMsts.Add(certify);
                    db.SaveChanges();
                    TempData["AddCertify"] = "Add successful.";
                    return RedirectToAction("Certify", "CertifyAdmin");
                }
                else
                {
                    TempData["AddCertifyError"] = "Add fail.";
                    return RedirectToAction("Certify", "CertifyAdmin");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            CertifyMst certifycoder = db.CertifyMsts.Where(row => row.Certify_ID == id).FirstOrDefault();
            return View(certifycoder);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(form["Certifyid"]);
                var CertifyType = form["CertifyType"];
                CertifyMst certifycoder = db.CertifyMsts.FirstOrDefault(row => row.Certify_ID == id);
                if (certifycoder != null)
                {
                    certifycoder.Certify_Type = form["CertifyType"];
                    db.SaveChanges();
                    TempData["EditCertify"] = "Update successful.";
                    return RedirectToAction("Certify", "CertifyAdmin");
                }
                else
                {
                    TempData["EditCertifyError"] = "Update fail.";
                    return RedirectToAction("Certify", "CertifyAdmin");
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            var certifyfy = db.CertifyMsts.Find(id);
            db.CertifyMsts.Remove(certifyfy);
            db.SaveChanges();
            TempData["DeleteCertify"] = "Delete successful.";
            return RedirectToAction("Certify", "CertifyAdmin");
        }
    }
}