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
        public ActionResult Manager()
        {
            return View();
        }
    }
}