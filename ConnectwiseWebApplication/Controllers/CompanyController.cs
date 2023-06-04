using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Controllers
{
    public class CompanyController : Controller
    {
        // GET: ComapnyRegisterationController
        CompanyFuntions CompanyFuntions=new CompanyFuntions();

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        
      
        public async Task<IActionResult> CompanyRegistration(Registration registration)
        {
            try
            {
                TempData["MyModelData"] = JsonConvert.SerializeObject(registration);
                bool res = await CompanyFuntions.CheckEmail(registration.companyEmail);
                if (res)
                {
                    ViewBag.flag = 1;
                    return View("Registration");
                }
                return RedirectToAction("Cards", "subscription");
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Error");
            }
        }

        public IActionResult CompanyRegistrationPaymentDone()
        {                          
               return RedirectToAction("Home", "Admin");                     
        }
            
    }
}
