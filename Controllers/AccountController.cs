using Jewelly.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Dynamic;

namespace Jewelly.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        JwelleyEntities db = new JwelleyEntities();
        public ActionResult Login()
        {
            if(TempData["register"] != null)
            {
                ViewBag.RegisterMg = TempData["register"];
            }
            if(TempData["registerError"] != null)
            {
                ViewBag.RegisterErrorMg = TempData["registerError"];
            }
            if (TempData["ChangeFrom"] != null)
            {
                ViewBag.ChangeFromMg = TempData["ChangeFrom"];
            }
            if(TempData["LoginError"] != null)
            {
                ViewBag.LoginError = TempData["LoginError"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string password, string username, string userId)
        {

            if (ModelState.IsValid)
            {

                var f_password = GetMD5(password);
                var data_cus = db.UserRegMsts.Where(s => s.Username == username && s.password.Equals(f_password));
                var data_ad = db.AdminLoginMsts.Where(s => s.userName == username && s.Password.Equals(f_password));

                if (data_cus.Count() > 0)
                {
                    //add session
                    Session["userID"] = data_cus.FirstOrDefault().userID;
                    Session["userFname"] = data_cus.FirstOrDefault().userFname;
                    Session["userLname"] = data_cus.FirstOrDefault().userLname;
                    Session["address"] = data_cus.FirstOrDefault().address;
                    Session["city"] = data_cus.FirstOrDefault().city;
                    Session["state"] = data_cus.FirstOrDefault().state;
                    Session["mobNo"] = data_cus.FirstOrDefault().mobNo;
                    Session["emailID"] = data_cus.FirstOrDefault().emailID;
                    Session["dob"] = data_cus.FirstOrDefault().dob;
                    Session["cdate"] = data_cus.FirstOrDefault().cdate;
                    Session["password"] = data_cus.FirstOrDefault().password;
                    Session["photo"] = data_cus.FirstOrDefault().photo;
                    Session["Username"] = data_cus.FirstOrDefault().Username;
                    Session["Path_photo"] = data_cus.FirstOrDefault().Path_photo;
                    return RedirectToAction("Index", "Home");
                }


                else if (data_ad.Count() > 0)
                {
                    //add session
                    Session["Name_employee"] = data_ad.FirstOrDefault().Name_employee;
                    Session["Avatar"] = data_ad.FirstOrDefault().Avatar;
                    Session["Path_avt"] = data_ad.FirstOrDefault().Path_avt;
                    Session["Birthday"] = data_ad.FirstOrDefault().Birthday;
                    Session["Email"] = data_ad.FirstOrDefault().Email;
                    Session["Phone"] = data_ad.FirstOrDefault().Phone;
                    Session["Address"] = data_ad.FirstOrDefault().Address;
                    Session["PasswordAd"] = f_password;
                    Session["userName"] = data_ad.FirstOrDefault().userName;
                    return RedirectToAction("Dashboard", "HomeAdmin", new { area = "Admin" });
                }
                else
                {
                    TempData["LoginError"] = "Login fail.";
                    return RedirectToAction("Login");
                }
            }
            TempData["LoginError"] = "Login fail.";
            return RedirectToAction("Login");

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form, UserRegMst user, string username)
        {
            if (ModelState.IsValid)
            {
                var email = form["emailID"];
                var check = db.UserRegMsts.FirstOrDefault(s => s.Username == username && s.emailID == email);
                if (check == null)
                {
                    user.userLname = form["userLname"];
                    user.userFname = form["userFname"];
                    user.address = form["address"];
                    user.city = form["city"];
                    user.state = "Online";
                    user.mobNo = form["mobNo"];
                    user.emailID = form["emailID"];
                    user.dob = form["dob"];
                    user.cdate = DateTime.Now.ToString();
                    user.password = GetMD5(form["password"]);
                    user.Username = form["Username"];
                    db.UserRegMsts.Add(user);
                    db.SaveChanges();
                    TempData["register"] = "Register Account successful.";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["registerError"] = "Username already exists";
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }

        public ActionResult General(int id)
        {
            if (TempData["Editinfor"] != null)
            {
                ViewBag.EditinforMg = TempData["Editinfor"];
            }
            if (TempData["ErrorEditinfor"] != null)
            {
                ViewBag.ErrorEditinforMg = TempData["ErrorEditinfor"];
            }
            if (TempData["changeps"] != null)
            {
                ViewBag.EditpMg = TempData["changeps"];
            }
            if (TempData["errorps"] != null)
            {
                ViewBag.ErrorpsMg = TempData["errorps"];
            }
            UserRegMst users = db.UserRegMsts.Where(s => s.userID == id).FirstOrDefault();
            return View(users);
        }
        public ActionResult Information()
        {

            var username = Session["Username"].ToString();
            UserRegMst users = db.UserRegMsts.Where(s => s.Username == username).FirstOrDefault();
            return View(users);
        }

        [HttpPost]
        public ActionResult Information(UserRegMst users)
        {
            var username = Session["Username"].ToString();
            var testuser = db.UserRegMsts.FirstOrDefault(s => s.Username == username);
            if (testuser != null)
            {
                UserRegMst user = db.UserRegMsts.Where(row => row.Username == username).FirstOrDefault();
                user.userLname = users.userLname;
                user.userFname = users.userFname;
                user.address = users.address;
                user.city = users.city;
                user.dob = users.dob;
                user.emailID = users.emailID;
                user.mobNo = users.mobNo;
                db.SaveChanges();
                TempData["Editinfor"] = "Change Information successful.";
                return RedirectToAction("General/" + user.userID, "Account");
            }
            else
            {
                TempData["ErrorEditinfor"] = "Change Information fail.";
                return RedirectToAction("Index", "Home");
            }


        }

        public ActionResult Change()
        {
            if (Session["Username"] == null)
            {
                return Content("You are not logged in, Please log in!");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Change(string Password, UserRegMst userReg, string New)
        {
            var username = Session["Username"].ToString();
            var cpass = GetMD5(Password);
            var newpass = GetMD5(New);
            var data = db.UserRegMsts.Where(s => s.Username == username && s.password == cpass);
            if (data != null)
            {

                var cus = db.UserRegMsts.Where(s => s.Username == username && s.password == newpass).FirstOrDefault();
                if (cus == null)
                {
                    var users = db.UserRegMsts.FirstOrDefault(s => s.Username == username);
                    users.password = newpass;
                    db.SaveChanges();
                    TempData["Password"] = "Change Password successful.";
                    return RedirectToAction("General/" + users.userID, "Account");

                }
                else
                {
                    TempData["PasswordError"] = "Change Password fail.";
                    return RedirectToAction("Change", "Account");

                }
            }

            else
            {
                return View("Login");

            }
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }

        private void SendEmail(string toEmail, string code)
        {
            string fromEmail = "pglaca22063@cusc.ctu.edu.vn"; // Điền địa chỉ email của bạn
            string fromPassword = "L@csocool"; // Điền mật khẩu email của bạn
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Yash Gems & Jewelry - Confirmation code forgot password";
            mailMessage.Body = $"\nYour confirmation code is: {code}\n\nDo not share this code with anyone!";
            smtpClient.Send(mailMessage);
        }
            
        public ActionResult Forget()
        {
            if(TempData["CheckMailError"] != null)
            {
                ViewBag.CheckMailError = TempData["CheckMailError"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Forget(FormCollection form)
        {
            var email = form["emailID"];
            var emailuser = db.UserRegMsts.Where(row => row.emailID == email).FirstOrDefault();
            if (emailuser != null)
            {
                string userEmail = email; // Lấy địa chỉ email từ người dùng
                string randomCode = GenerateRandomCode(); // Sinh mã xác nhận ngẫu nhiên
                SendEmail(userEmail, randomCode); // Gửi email chứa mã xác nhận
                                                  // Lưu mã xác nhận vào cơ sở dữ liệu hoặc session để kiểm tra sau này
                Session["VerificationCode"] = randomCode;
                Session["EmailForget"] = email;
                TempData["CheckMail"] = "Email has been sent";
                return RedirectToAction("Verification", "Account");
            }
            else
            {
                TempData["CheckMailError"] = "Email don't exist";
                return RedirectToAction("Forget", "Account");
            }
            // Chuyển hướng đến trang nhập lại hoặc trang xác nhận mã
            //Response.Redirect("Forget.cshtml");
            
        }

        public ActionResult Verification()
        {
            if(TempData["VerificationCodeError"] != null)
            {
                ViewBag.VerificationErrorMg = TempData["VerificationCodeError"];
            }
            if (TempData["CheckMail"] != null)
            {
                ViewBag.CheckMailer = TempData["CheckMail"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Verification(FormCollection form)
        {
            string one = form["one"];
            string two = form["two"];
            string three = form["three"];
            string four = form["four"];
            string five = form["five"];
            string six = form["six"];
            string total = one + two + three + four + five + six;
            string ses = Session["VerificationCode"] as string;
            if (total == ses)
            {
                Session.Remove("VerificationCode");
                return RedirectToAction("ChangeFromForget", "Account");
            }
            else
            {
                TempData["VerificationCodeError"] = "Verification Code not match";
                return RedirectToAction("Verification", "Account");
            }

        }

        public ActionResult ChangeFromForget()
        {
            if(TempData["ChangeFromError"] != null)
            {
                ViewBag.ChangeFromErrorMg = TempData["ChangeFromError"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangeFromForget(FormCollection form)
        {
            var newpass = GetMD5(form["news"]);
            var confirm = GetMD5(form["confirm"]);
            string email = Session["EmailForget"] as string;
            if(newpass == confirm)
            {
                var updatePass = db.UserRegMsts.Where(row => row.emailID.Equals(email)).FirstOrDefault();
                var updatePassAdmin = db.AdminLoginMsts.Where(row => row.Email.Equals(email)).FirstOrDefault();
                if (updatePass != null)
                {
                    updatePass.password = newpass;
                }
                else if(updatePassAdmin != null) { }
                {
                    updatePassAdmin.Password = newpass;
                }
                db.SaveChanges();
                Session.Remove("EmailForget");
                Session.Remove("VerificationCode");
                TempData["ChangeFrom"] = "Change Password successful.";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["ChangeFromError"] = "Change Password fail.";
                return RedirectToAction("ChangeFromForget", "Account");
            }
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
                return RedirectToAction("General", "Account", new { id });
                //return Json(new { Message = "File(s) uploaded successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error uploading files", Error = ex.Message });
            }
        }
        public ActionResult Logout(string userId)
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "Account");
        }

        public ActionResult MyPurchase(int id)
        {
            var order = new JoinMyPurchase().SelectPurchase(id).ToList();
            return View(order);
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