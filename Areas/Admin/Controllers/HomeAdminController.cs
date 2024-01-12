using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace Jewelly.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        JwelleyEntities db = new JwelleyEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        public ActionResult Inquiry(string search = "")
        {
            List<Inquiry> inquiry = db.Inquiries.Where(row => row.Name.Contains(search)).ToList();
            return View(inquiry);
        }

        public ActionResult News(string search = "")
        {
            List<News> news = db.News.Where(row => row.NGmail.Contains(search)).ToList();
            return View(news);
        }

        public ActionResult General()
        {
            if (TempData["UpdateInfor"] != null)
            {
                ViewBag.UpdateInforMg = TempData["UpdateInfor"];
            }
            if (TempData["UpdateInforError"] != null)
            {
                ViewBag.UpdateInforErrorMg = TempData["UpdateInforError"];
            }
            if (TempData["ChangePassHome"] != null)
            {
                ViewBag.ChangePassHomeMg = TempData["ChangePassHome"];
            }
            if (TempData["ChangePassHomeError"] != null)
            {
                ViewBag.ChangePassHomeErrorMg = TempData["ChangePassHomeError"];
            }
            string userNames = Session["userName"] as string;
            AdminLoginMst admins = db.AdminLoginMsts.Where(row => row.userName == userNames).FirstOrDefault();
            return View(admins);
        }

        [HttpPost]
        public ActionResult UploadFiles()
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
                        string name = Session["userName"] as string;
                        var user = db.AdminLoginMsts.FirstOrDefault(row => row.userName == name);
                        if (user != null)
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
                return RedirectToAction("General", "HomeAdmin");
                //return Json(new { Message = "File(s) uploaded successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error uploading files", Error = ex.Message });
            }
        }


        public ActionResult Change()
        {
            string userNames = Session["userName"] as string;
            AdminLoginMst admins = db.AdminLoginMsts.Where(row => row.userName == userNames).FirstOrDefault();
            return View(admins);
        }
        [HttpPost]
        public ActionResult Change(string password, AdminLoginMst userReg, string newp)
        {
            var username = Session["userName"].ToString();
            var cpass = GetMD5(password);
            var newpass = GetMD5(newp);
            var data = db.AdminLoginMsts.Where(s => s.userName == username && s.Password == cpass);
            if (data != null)
            {
                var cus = db.AdminLoginMsts.Where(s => s.userName == username && s.Password == newpass).FirstOrDefault();
                if (cus == null)
                {
                    var users = db.AdminLoginMsts.FirstOrDefault(s => s.userName == username);
                    users.Password = newpass;
                    db.SaveChanges();
                    TempData["ChangePassHome"] = "Change successful.";
                    return RedirectToAction("General", "HomeAdmin");
                }
                else
                {
                    TempData["ChangePassHomeError"] = "Change fail.";
                    return RedirectToAction("General", "HomeAdmin");
                }
            }
            else
            {
                return RedirectToAction("General", "HomeAdmin");
            }
        }

        public ActionResult Information()
        {
            var username = Session["userName"].ToString();
            var users = db.AdminLoginMsts.Where(s => s.userName == username).FirstOrDefault();
            return View(users);
        }
        [HttpPost]
        public ActionResult Information(AdminLoginMst users, HttpPostedFileBase imageFiles)
        {
            var username = Session["userName"].ToString();
            var testuser = db.AdminLoginMsts.FirstOrDefault(s => s.userName == username);
            if (testuser != null)
            {
                testuser.Name_employee = users.Name_employee;
                testuser.Address = users.Address;
                testuser.Birthday = users.Birthday;
                testuser.Email = users.Email;
                testuser.Phone = users.Phone;
                db.SaveChanges();
                TempData["UpdateInfor"] = "Update successful.";
                return RedirectToAction("General", "HomeAdmin");
            }
            else
            {
                TempData["UpdateInforError"] = "Update fall.";
                return RedirectToAction("Information", "HomeAdmin");
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
        [HttpGet]
        public ActionResult GetChartData()
        {
            var result = db.CartLists
               .GroupBy(x => new { Year = DbFunctions.TruncateTime(x.OrderDate).Value.Year, Month = DbFunctions.TruncateTime(x.OrderDate).Value.Month })
               .Select(g => new
               {
                   Year = g.Key.Year,
                   Month = g.Key.Month,
                   TotalRevenue = g.Sum(x => x.MRP)
               })
               .OrderBy(x => x.Year)
               .ThenBy(x => x.Month)
               .ToList();
            var totalRevenue = result.Sum(x => x.TotalRevenue);
            var resultWithTotalRevenue = new
            {
                Result = result,
                TotalRevenue = totalRevenue
            };
            db.Configuration.LazyLoadingEnabled = false;
            return Json(resultWithTotalRevenue, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Notification()
        {
            if (Session["username"] != null)
            {
                var pending = db.CartLists.Where(row => row.Status.Equals("pending")).Count();
                ViewBag.QuantityCart = pending;
            }
            return PartialView("Notification");
        }

        public ActionResult Notify()
        {
            if(TempData["StatusApproved"] != null)
            {
                ViewBag.StatusApprovedMg = TempData["StatusApproved"];
            }
            if (TempData["StatusApprovedError"] != null)
            {
                ViewBag.StatusApprovedErrorMg = TempData["StatusApprovedError"];
            }
            if(TempData["StatusCancel"] != null)
            {
                ViewBag.StatusCancelMg = TempData["StatusCancel"];
            }
            if (TempData["StatusCancelError"] != null)
            {
                ViewBag.StatusCancelErrorMg = TempData["StatusCancelError"];
            }
            List<CartList> cartLists = db.CartLists.Where(row => row.Status.Contains("Pending")).ToList();
            return View(cartLists);
        }

        public ActionResult Accept(int id)
        {
            var item = db.CartLists.Where(row => row.ID == id).FirstOrDefault();
            if(item != null)
            {
                item.Status = "Approved";
                db.SaveChanges();
                TempData["StatusApproved"] = "Update successful.";
                return RedirectToAction("Notify", "HomeAdmin");
            }
            else
            {
                TempData["StatusApprovedError"] = "Update fail.";
                return RedirectToAction("Notify", "HomeAdmin");
            }
        }

        public ActionResult Cancel(int id)
        {
            var item = db.CartLists.Where(row => row.ID == id).FirstOrDefault();
            if (item != null)
            {
                item.Status = "Cancel";
                db.SaveChanges();
                TempData["StatusCancel"] = "Update successful.";
                return RedirectToAction("Notify", "HomeAdmin");
            }
            else
            {
                TempData["StatusCancelError"] = "Update fail.";
                return RedirectToAction("Notify", "HomeAdmin");
            }
        }
    }
}