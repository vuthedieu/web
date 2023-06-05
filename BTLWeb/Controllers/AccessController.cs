using Microsoft.AspNetCore.Mvc;
using BTLWeb.Models;
using System.Net;
using System.Net.Mail;

namespace BTLWeb.Controllers
{
    public class AccessController : Controller
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("Username")==null)
            {
                /*var f_password = GetMD5(password);*/
                var data = db.TUsers.Where(s => s.Username.Equals(user.Username) && s.Password.Equals(user.Password)).FirstOrDefault();
                if (data != null)
                {
                    HttpContext.Session.SetString("Username", data.Username.ToString());
                    //add session
                    if (data.LoaiUser == 0)
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "Access");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OTP()
        {
            return View();
        }

        public class OTPClass
        {
            public string otpGet { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyOTP(OTPClass oTPClass)
        {
            if (ModelState.IsValid)
            {
                var otp = oTPClass.otpGet;
                var otpcheck = HttpContext.Session.GetString("OTPMail");
                if (otp.Equals(otpcheck))
                {
                    var username = HttpContext.Session.GetString("UserCheck");
                    var password = HttpContext.Session.GetString("PasswordCheck");
                    TUser user = new TUser();
                    user.Username = username;
                    user.Password = password;
                    user.LoaiUser = 1;
                    db.TUsers.Add(user);
                    db.SaveChanges();
                    HttpContext.Session.Clear();//remove session
                    HttpContext.Session.Remove("OTPMail");
                    HttpContext.Session.Remove("UserCheck");
                    HttpContext.Session.Remove("PasswordCheck");
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    ViewBag.error = "OTP incorrect";
                    return RedirectToAction("OTP", "Access");
                }
            }
            return RedirectToAction("OTP", "Access");
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TUser user)
        {
            if (ModelState.IsValid)
            {
                var check = db.TUsers.FirstOrDefault(s => s.Username == user.Username);
                if (check == null)
                {
                   
                    var otpmail = RandomNumber(6);
                    HttpContext.Session.SetString("OTPMail", otpmail);
                    HttpContext.Session.SetString("UserCheck", user.Username);
                    HttpContext.Session.SetString("PasswordCheck", user.Password);
                    SendMail(user.Username, "Xác thực đăng ký", "Mã OTP xác thực của bạn là : " + otpmail);
                    return RedirectToAction("OTP", "Access");
                }
                else
                {
                    ViewBag.error = "Username already exists";
                    return View();
                }

            }
            return View();


        }

        public static string RandomNumber(int numberRD)
        {
            string randomStr = "";
            try
            {

                int[] myIntArray = new int[numberRD];
                int x;
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }

        //thư viện gửi mail - có sẵn, thay thông tin của mail chủ mình vào
        public void SendMail(String to, String subject, String content)
        {
            String mailAddress = "hiptrangpt@gmail.com";
            String mailPassword = "qiwsxspjamjelrcb"; // lấy mk ứng dụng từ tài khoản gg 

            MailMessage msg = new MailMessage(new MailAddress(mailAddress), new MailAddress(to));
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = content;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(mailAddress, mailPassword);
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Send(msg);
        }
    }
}
