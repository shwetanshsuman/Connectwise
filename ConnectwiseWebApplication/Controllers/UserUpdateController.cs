using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectwiseWebApplication.Controllers
{
    public class UserUpdateController : Controller
    {
        public IActionResult UpdatePassword(int userId)
        {
            try
            {
                return View(new { id = userId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            try
            {
                password = PasswordEncoding.EncodePasswordToBase64(password);
                UserFunctions userFunctions = new UserFunctions();
                bool status = await userFunctions.UpdateUserPassword(id, password);
                if (status)
                {
                    return RedirectToAction("Index", "Login");
                }
                return RedirectToAction("Index", "Error"); ;
            } 
            catch (Exception) 
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
