using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Admin/Administrator
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Manager(string search="")
        {
            if (TempData["delete"] != null)
            {
                ViewBag.DeleteMg = TempData["delete"];
            }
            if (TempData["Add"] != null)
            {
                ViewBag.AddMg = TempData["Add"];
            }
            if (TempData["ErrorAdd"] != null)
            {
                ViewBag.ErrorAddMg = TempData["ErrorAdd"];
            }
            List<AdminLoginMst> AdminLoginMst = db.AdminLoginMsts.Where(row => row.userName.Contains(search)).ToList();
            return View(AdminLoginMst);
        }
        
       public ActionResult Details(int id)
        {
            if(TempData["Infor"] != null)
            {
                ViewBag.InforMg = TempData["Infor"];
            }
            if(TempData["Pass"] != null)
            {
                ViewBag.PassMg = TempData["Pass"];
            }
            if(TempData["ErrorPass"] != null)
            {
                ViewBag.ErrorPass = TempData["ErrorPass"];
            }
            if (TempData["Image"] != null)
            {
                ViewBag.ImageMg = TempData["Image"];
            }
            if(TempData["InforErrorMatch"] != null)
            {
                ViewBag.InforErrorMatch = TempData["InforErrorMatch"];
            }
            var adminLoginMst = db.AdminLoginMsts.Where(row => row.AdminID == id).FirstOrDefault();
            //ViewBag.ImagePath = adminLoginMst.Path_avt + adminLoginMst.Avatar;
            return View(adminLoginMst);
        }

        public ActionResult Delete(int id)
        {
            var adminLoginMst = db.AdminLoginMsts.Find(id);
            db.AdminLoginMsts.Remove(adminLoginMst);
            db.SaveChanges();
            TempData["delete"] = "Delete successfully!";
            return RedirectToAction("Manager", "Administrator");
        }

        [HttpPost]
        public ActionResult UploadFiles(int id)
        {
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        // Đường dẫn lưu trữ file trên server (điều này cần được điều chỉnh tùy theo yêu cầu của bạn)
                        var uploadPath = Server.MapPath("/Content/Image/Admin/");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        
                        var user = db.AdminLoginMsts.FirstOrDefault(row => row.AdminID == id);
                            if(user != null)
                            {
                                string filename = user.userName + ".jpg";
                                var filePath = Path.Combine(Server.MapPath("/Content/Image/Admin/"), filename);
                                file.SaveAs(filePath);
                                user.Path_avt = "/Content/Image/Admin/";
                                user.Avatar = filename;
                                db.SaveChanges();
                                TempData["Image"] = "Update successful.";
                            }
                        // Xử lý file tại đây, ví dụ: lưu thông tin vào cơ sở dữ liệu, vv.
                    }
                   
                }
                return RedirectToAction("Details", "Administrator", new {id});
                //return Json(new { Message = "File(s) uploaded successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error uploading files", Error = ex.Message });
            }
        }

        public ActionResult General(int id)
        {
            var adminLoginMst = db.AdminLoginMsts.Where(row => row.AdminID == id).FirstOrDefault();
            return View(adminLoginMst);
        }

        [HttpPost]
        public ActionResult General(FormCollection form)
        {
            int id = int.Parse(form["id"]);
            var admin = db.AdminLoginMsts.Where(s => s.AdminID == id).FirstOrDefault();
            if (admin != null)
            {
                admin.Name_employee = form["name"];
                admin.Email = form["emalID"];
                admin.Phone = form["mobNo"];
                admin.Birthday = form["dob"];
                admin.Address = form["address"];
                db.SaveChanges();
                TempData["Infor"] = "Change General successful";
                return RedirectToAction("Details/" + id, "Administrator");

            }
            else
            {
                TempData["InforErrorMatch"] = "Change General fail";
                return RedirectToAction("Details/" + id, "Administrator");
            }
        
        }

        public ActionResult Change(int id)
        {
            var adminLoginMst = db.AdminLoginMsts.Where(row => row.AdminID == id).FirstOrDefault();
            return View(adminLoginMst);
        }

        [HttpPost]
        public ActionResult Change(FormCollection form)
        {
            int id = int.Parse(form["id"]);
            var pass = GetMD5(form["pass"]);
            var cpass = GetMD5(form["cpass"]);
            var admin = db.AdminLoginMsts.Where(s => s.AdminID == id && s.Password != pass).FirstOrDefault();
            if(admin != null)
            {
                if(pass == cpass)
                {
                    admin.Password = pass;
                    db.SaveChanges();
                    TempData["Pass"] = "Change Password successful";
                    return RedirectToAction("Details/" + id,"Administrator");
                }
                else
                {
                    TempData["ErrorPass"] = "Password Match";
                    return RedirectToAction("Details/" + id, "Administrator");
                }
            }
            else
            {
                TempData["ErrorPass"] = "Password Match";
                return RedirectToAction("Details/" + id, "Administrator");
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var user = form["username"];
            var email = form["emalID"];
            var check = db.AdminLoginMsts.Where(s => s.userName == user && s.Email == email).FirstOrDefault();
            if(check == null)
            {
                AdminLoginMst admin = new AdminLoginMst();
                admin.Name_employee = form["name"];
                admin.userName = form["username"];
                admin.Password = GetMD5(form["password"]);
                admin.Email = form["emalID"];
                admin.Phone = form["mobNo"];
                admin.Birthday = form["dob"];
                admin.Address = form["address"];
                admin.Path_avt = "/Areas/Admin/Content/Image/";
                admin.Avatar = "avatardefault.jpg";
                db.AdminLoginMsts.Add(admin);
                db.SaveChanges();
                TempData["Add"] = "Add Administrator successful.";
                return RedirectToAction("Manager", "Administrator");
            }
            else
            {
                TempData["ErrorAdd"] = "Add Administrator fail.";
                return RedirectToAction("Manager", "Administrator");
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}