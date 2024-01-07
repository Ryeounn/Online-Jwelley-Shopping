using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Admin/Administrator
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Manager()
        {
            List<AdminLoginMst> AdminLoginMst = db.AdminLoginMsts.ToList();
            return View(AdminLoginMst);
        }

        
        public ActionResult Edit(string user)
        {
            AdminLoginMst Admin = db.AdminLoginMsts.Where(s => s.userName == user).FirstOrDefault();
            return View(Admin);
        }


    }
}