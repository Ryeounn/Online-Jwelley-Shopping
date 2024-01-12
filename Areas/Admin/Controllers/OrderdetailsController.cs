using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class OrderdetailsController : Controller
    {
        JwelleyEntities db = new JwelleyEntities();

        // GET: Admin/Orderdetails
        public ActionResult Orderdetails(string oc="")
        {
            List<ProdMst> prods = db.ProdMsts.ToList();
            //  var orderDetail = db.Orderdetails.Include(od => od.ItemMst).Include(od => od.ItemMst.ProdMst).ToList();
            List<Orderdetail> orderdetails = db.Orderdetails.Where(s=>s.ItemMst.ProdMst.Prod_Type.Contains(oc)).ToList();
            return View(orderdetails);
        }
      
      
        public ActionResult Details(int id)
        {
            var item = db.Orderdetails.Where(s=>s.Orderdetails_ID == id).FirstOrDefault();
            return View(item);
        }
    }
}