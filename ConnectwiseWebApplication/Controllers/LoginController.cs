using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConnectwiseWebApplication.Controllers
{
    public class LoginController : Controller
    {
        CookieOptions cookieOptions = new CookieOptions();
        UserFunctions userFunctions = new UserFunctions();

        public ActionResult Index()
        {
            ViewData["Flag"] = 0;
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("LoginCheck", "Login");
            return View();
        }
        public async Task<IActionResult> LoginCheck()
        {
            try
            {
                int? id = Convert.ToInt32(HttpContext.Request.Cookies["user_Id"].ToString());
                User user = await userFunctions.GetUserById(id);
                if (user.RoleId == 2)
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (user.RoleId == 3)
                {

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var res = await userFunctions.ValidateUser(user, HttpContext);
                if (res == 2)
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (res == 3)
                {

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    // ViewBag.Message = "Invalid Email or Password";
                    ViewData["Flag"] = 1;
                }
                return View("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (HttpContext.Request.Cookies["user_Id"] != null || HttpContext.Request.Cookies["Company_id"] != null || HttpContext.Request.Cookies["UserName"] != null)
                {
                    cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(-30));
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception) 
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string userEmail)
        {
            try
            {
                IEnumerable<User> users = await userFunctions.GetUsers();
                User user = users.FirstOrDefault(x => x.UserName == userEmail);
                if (user == null)
                {
                    ViewData["flag"] = 0;
                    ViewData["Message"] = "Email does not exist";
                }
                else
                {
                    string resetPasswordLink = "https://localhost:7081/UserUpdate/UpdatePassword/" + user.UserId;
                    string redirectToLogin = "https://localhost:7081/";
                    MasterEmail masterEmail;
                    EmailFunction emailFunction = new EmailFunction();
                    masterEmail = await emailFunction.GetEmailById(5);
                    string body = masterEmail.EmailBody;
                    body = String.Format(body, resetPasswordLink, redirectToLogin);
                    EmailExecution emailExecution = new EmailExecution();
                    Email email = new Email()
                    {
                        MailFrom = "connectwiseadmin@gmail.com",
                        MailFromName = "ConnectWise Admin",
                        MailTo = userEmail,
                        Subject = masterEmail.EmailSubject,

                        Body = body
                    };
                    emailExecution.SendEmail(email);
                    ViewData["flag"] = 1;
                    ViewData["Message"] = "Check your email";
                }
                return View("ForgetPassword");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}

