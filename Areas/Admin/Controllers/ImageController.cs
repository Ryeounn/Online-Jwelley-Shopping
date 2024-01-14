using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Jewelly.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        // GET: Admin/Image
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Image()
        {
            List<ItemMst> images = db.ItemMsts.ToList();
            return View(images);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Img img,HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5, HttpPostedFileBase Image6)
        {
            if (ModelState.IsValid)
            {
                    Img imgs = new Img();
                    string filename = img.pic_1 + "_1.jpg";
                    imgs.pic_1 = filename;
                    string path1 = Path.Combine(Server.MapPath("/Content/Image/Product/Items/"), filename);
                    Image1.SaveAs(path1);
                    //img2
                    string filename1 = img.pic_2 + "_2.jpg";
                    imgs.pic_2 = filename1;
                    string path2 = Path.Combine(Server.MapPath("/Content/Image/Product/Items/"), filename1);
                    Image2.SaveAs(path2);
                    //img3
                    string filename2 = img.pic_3 + "_3.jpg";
                    imgs.pic_3 = filename2;
                    string path3 = Path.Combine(Server.MapPath("/Content/Image/Product/Items/"), filename2);
                    Image3.SaveAs(path3);
                    //img4
                    string filename3 = img.pic_4 + "_4.jpg";
                    imgs.pic_4 = filename3;
                    string path4 = Path.Combine(Server.MapPath("/Content/Image/Product/Items"), filename3);
                    Image4.SaveAs(path4);
                    //img5
                    string filename4 = img.pic_5 + "_5.jpg";
                    imgs.pic_5 = filename4;
                    string path5 = Path.Combine(Server.MapPath("/Content/Image/Product/Items"), filename4);
                    Image5.SaveAs(path5);
                    //img6
                    string filename5 = img.pic_6 + "_6.jpg";
                    imgs.pic_6 = filename5;
                    string path6 = Path.Combine(Server.MapPath("/Content/Image/Product/Items"), filename5);
                    Image6.SaveAs(path6);


                    db.Imgs.Add(imgs);
                    db.SaveChanges();
                    TempData["Add"] = "Add Imgs succesful.";
                    return RedirectToAction("Image", "Image");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {       
            var img = db.Imgs.Where(s=>s.ID == id).FirstOrDefault();
            return View(img);
        }
        [HttpPost]
        public ActionResult Edit(Img img)
        {
            Img imgs = db.Imgs.Where(row => row.ID == img.ID).FirstOrDefault();
            if (imgs != null)
            {
                
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