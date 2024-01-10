using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Areas.Admin.Controllers
{
    public class CustomerAdminController : Controller
    {
        // GET: Admin/CustomerAdmin
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Customer(string search = "")
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
            List<UserRegMst> userRegMsts = db.UserRegMsts.Where(row => row.Username.Contains(search)).ToList();
            return View(userRegMsts);
        }

        public ActionResult Details(int id)
        {
            if (TempData["Infor"] != null)
            {
                ViewBag.InforMg = TempData["Infor"];
            }
            if (TempData["Pass"] != null)
            {
                ViewBag.PassMg = TempData["Pass"];
            }
            if (TempData["ErrorPass"] != null)
            {
                ViewBag.ErrorPass = TempData["ErrorPass"];
            }
            if (TempData["Image"] != null)
            {
                ViewBag.ImageMg = TempData["Image"];
            }
            if (TempData["InforErrorMatch"] != null)
            {
                ViewBag.InforErrorMatch = TempData["InforErrorMatch"];
            }
            var userRegMst = db.UserRegMsts.Where(row => row.userID == id).FirstOrDefault();
            //ViewBag.ImagePath = adminLoginMst.Path_avt + adminLoginMst.Avatar;
            return View(userRegMst);
        }

        public ActionResult Delete(int id)
        {
            var userRegMst = db.UserRegMsts.Find(id);
            db.UserRegMsts.Remove(userRegMst);
            db.SaveChanges();
            TempData["delete"] = "Delete successfully!";
            return RedirectToAction("Customer", "CustomerAdmin");
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
                        var uploadPath = Server.MapPath("/Content/Image/Customer/");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        var user = db.UserRegMsts.FirstOrDefault(row => row.userID == id);
                        if (user != null)
                        {
                            string filename = user.Username + ".jpg";
                            var filePath = Path.Combine(Server.MapPath("/Content/Image/Customer/"), filename);
                            file.SaveAs(filePath);
                            user.Path_photo = "/Content/Image/Customer/";
                            user.photo = filename;
                            db.SaveChanges();
                            TempData["Image"] = "Update successful.";
                        }
                        // Xử lý file tại đây, ví dụ: lưu thông tin vào cơ sở dữ liệu, vv.
                    }

                }
                return RedirectToAction("Details", "CustomerAdmin", new { id });
                //return Json(new { Message = "File(s) uploaded successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error uploading files", Error = ex.Message });
            }
        }

        public ActionResult General(int id)
        {
            var userRegMst = db.UserRegMsts.Where(row => row.userID == id).FirstOrDefault();
            return View(userRegMst);
        }

        [HttpPost]
        public ActionResult General(FormCollection form)
        {
            int id = int.Parse(form["id"]);
            var user = db.UserRegMsts.Where(s => s.userID == id).FirstOrDefault();
            if (user != null)
            {
                    user.userFname = form["first"];
                    user.userLname = form["last"];
                    user.emailID = form["emalID"];
                    user.mobNo = form["mobNo"];
                    user.dob = form["dob"];
                    user.address = form["address"];
                    db.SaveChanges();
                    TempData["Infor"] = "Change General successful";
                    return RedirectToAction("Details/" + id, "CustomerAdmin");
            }
            else
            {
                    TempData["InforErrorMatch"] = "Change General fail";
                    return RedirectToAction("Details/" + id, "CustomerAdmin");
            }
}

        public ActionResult Change(int id)
        {
            var userRegMst = db.UserRegMsts.Where(row => row.userID == id).FirstOrDefault();
            return View(userRegMst);
        }

        [HttpPost]
        public ActionResult Change(FormCollection form)
        {
            int id = int.Parse(form["id"]);
            var pass = GetMD5(form["pass"]);
            var cpass = GetMD5(form["cpass"]);
            var user = db.UserRegMsts.Where(s => s.userID == id && s.password != pass).FirstOrDefault();
            if (user != null)
            {
                if (pass == cpass)
                {
                    user.password = pass;
                    db.SaveChanges();
                    TempData["Pass"] = "Change Password successful";
                    return RedirectToAction("Details/" + id, "CustomerAdmin");
                }
                else
                {
                    TempData["ErrorPass"] = "Password Match";
                    return RedirectToAction("Details/" + id, "CustomerAdmin");
                }
            }
            else
            {
                TempData["ErrorPass"] = "Password Match";
                return RedirectToAction("Details/" + id, "CustomerAdmin");
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var users = form["username"];
            var email = form["emalID"];
            var check = db.UserRegMsts.Where(s => s.Username == users && s.emailID == email).FirstOrDefault();
            if (check == null)
            {
                UserRegMst user = new UserRegMst();
                user.userFname = form["name"];
                user.userLname = form["last"];
                user.Username = form["username"];
                user.password = GetMD5(form["password"]);
                user.emailID = form["emalID"];
                user.mobNo = form["mobNo"];
                user.dob = form["dob"];
                user.state = "Offline";
                user.city = form["city"];
                user.cdate = DateTime.Now.ToString();
                user.address = form["address"];
                user.Path_photo = "/Areas/Admin/Content/Image/";
                user.photo = "avatardefault.jpg";
                db.UserRegMsts.Add(user);
                db.SaveChanges();
                TempData["Add"] = "Add Customer successful.";
                return RedirectToAction("Customer", "CustomerAdmin");
            }
            else
            {
                TempData["ErrorAdd"] = "Add Customer fail.";
                return RedirectToAction("Custmoer", "CustomerAdmin");
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