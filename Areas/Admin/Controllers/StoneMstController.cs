using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class StoneMstController : Controller
    {
        // GET: Admin/StoneMst
            JwelleyEntities db = new JwelleyEntities();
            // GET: Admin/Default
            public ActionResult Stone()
            {
                if(TempData["AddStoneMst"] != null)
                {
                    ViewBag.AddStoneMstMg = TempData["AddStoneMst"];
                }
                if(TempData["AddStoneMstError"] != null)
                {
                    ViewBag.AddStoneMstErrorMg = TempData["AddStoneMstError"];
                }
                if(TempData["EditStoneMst"] != null)
                {
                    ViewBag.EditStoneMstMg = TempData["EditStoneMst"];
                }
                if(TempData["EditStoneMstError"] != null)
                {
                    ViewBag.EditStoneMstErrorMg = TempData["EditStoneMstError"];
                }
                if(TempData["deleteStone"] != null)
                {
                    ViewBag.DeleteStoneMg = TempData["deleteStone"];
                }
                List<StoneMst> stone = db.StoneMsts.ToList();
                return View(stone);
            }


            public ActionResult Add()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Add(StoneMst p, FormCollection form)
            {
                if (ModelState.IsValid)
                {
                    var stone1 = db.StoneMsts.FirstOrDefault(row => row.Stone_ID == p.Stone_ID);
                    if (stone1 == null)
                    {
                        StoneMst stone = new StoneMst();
                        stone.Stone_Gm = decimal.Parse(form["Stone_Gm"]);
                        stone.Stone_Crt = decimal.Parse(form["Stone_Crt"]);
                        stone.Stone_Rate = decimal.Parse(form["Stone_Rate"]);
                        stone.Stone_Amt = decimal.Parse(form["Stone_Amt"]);
                        stone.Style_Code = p.Style_Code;
                        stone.StoneQlty_ID = p.StoneQlty_ID;
                        db.StoneMsts.Add(p);
                        db.SaveChanges();
                        TempData["AddStoneMst"] = "Add successful.";
                        return RedirectToAction("Stone", "StoneMst");
                    }
                    else
                    {
                        TempData["AddStoneMstError"] = "Add fail.";
                        return RedirectToAction("Create", "StoneMst");

                    }
                }
                return View();


            }
            public ActionResult Edit(int id)
            {
                StoneMst sto = db.StoneMsts.Where(row => row.Stone_ID == id).FirstOrDefault();
                return View(sto);
            }

            [HttpPost]
            public ActionResult Edit(StoneMst pro)
            {
                var stoneMst = db.StoneMsts.Where(row => row.Stone_ID == pro.Stone_ID).FirstOrDefault();
                //update
                if (stoneMst != null)
                {
                    StoneMst st = db.StoneMsts.Where(rows => rows.Stone_ID == pro.Stone_ID).FirstOrDefault();
                    st.Stone_Gm = pro.Stone_Gm;
                    st.Stone_Pcs = pro.Stone_Pcs;
                    st.Stone_Crt = pro.Stone_Crt;
                    st.Stone_Rate = pro.Stone_Rate;
                    st.Stone_Amt = pro.Stone_Amt;
                    st.Style_Code = pro.Style_Code;
                    st.StoneQlty_ID = pro.StoneQlty_ID;
                    db.SaveChanges();
                    TempData["EditStoneMst"] = "Update successful.";
                    return RedirectToAction("Stone", "StoneMst");
                }
                else
                {
                    TempData["EditStoneMstError"] = "Update fail.";
                    return RedirectToAction("Stone", "StoneMst");
                }
            }

            public ActionResult Delete(int id)
            {
                StoneMst prodel = db.StoneMsts.Find(id);
                db.StoneMsts.Remove(prodel);
                db.SaveChanges();
                TempData["deleteStone"] = "Delete successful.";
                return RedirectToAction("Stone","StoneMst");
            }
    }
}