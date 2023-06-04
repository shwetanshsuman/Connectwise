using ConnectWiseBackend.Models;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.Schedule;
using System.Runtime.InteropServices;
using Subscription = ConnectwiseWebApplication.Models.Subscription;
using Company = ConnectwiseWebApplication.Models.Company;

namespace ConnectwiseWebApplication.Controllers
{
    public class SubscriptionController : Controller
    {
        SubscriptionFunctions subscriptionFunctions=new SubscriptionFunctions();
       
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Subscription> subscriptions = await subscriptionFunctions.GetSubscriptions();
                return View(subscriptions);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

		public async Task<IActionResult> Cards()
		{
            try
            {
                IEnumerable<Subscription> subscriptions = await subscriptionFunctions.GetSubscriptions();
                var data = TempData["MyModelData"] as string;
                var registration = JsonConvert.DeserializeObject<Registration>(data);
                RegisterationSubscription registrationSubscription = new RegisterationSubscription();

                if (registration == null)
                {
                    RedirectToAction("CompanyRegistration", "Company");
                }
                else
                {

                    registrationSubscription.registration = registration;
                    registrationSubscription.subscription = subscriptions;

                }
                TempData["Registeration"] = JsonConvert.SerializeObject(registrationSubscription.registration);

                return View(registrationSubscription);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

		public async Task<ActionResult> Details(int id)
        {
            try
            {
                var data = TempData["Registeration"] as string;

                Registration registration = JsonConvert.DeserializeObject<Registration>(data);
                PaymentModel paymentModel = new PaymentModel();

                Subscription subscription = await subscriptionFunctions.GetSubscriptionById(id);
                ViewBag.Subscriptionid = subscription.SubscriptionId;
                ViewBag.SubscriptionFess = subscription.SubscriptionFees;

                RegistrationPayment registrationPayment = new RegistrationPayment();

                registrationPayment.Payment = paymentModel;
                TempData["registration"] = JsonConvert.SerializeObject(registration);
                TempData["TotalAmount"] = (subscription.SubscriptionFees).ToString();
                TempData["Subscriptionid"] = (subscription.SubscriptionId).ToString();


                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }


        public async  Task<IActionResult> PaymentForm(RegistrationPayment registrationPayment)
        {
            try
            {
                var data = TempData["registration"] as string;
                var subscriptionId = Convert.ToInt32(TempData["Subscriptionid"] as string);
                var totatalamount = Convert.ToInt32(TempData["TotalAmount"] as string);
                var registration = JsonConvert.DeserializeObject<Registration>(data);
                CompanyFuntions companyFuntions = new CompanyFuntions();
                var subdata = companyFuntions.Compnayregistrationwithsubscription(registration, HttpContext);
                if (subdata != null)
                {
                    SubscriptionFunctions subscriptionFunctions = new SubscriptionFunctions();
                    bool purchasestatus = await subscriptionFunctions.SubscriptionPurchase(registrationPayment, subdata.Result.Item1, data, totatalamount, HttpContext);
                    if (purchasestatus)
                    {
                        Company company = await companyFuntions.GetCompanyByid(subdata.Result.Item2);
                        bool result = companyFuntions.updateCompany(company, subscriptionId);
                        if (result)
                        {
                            return RedirectToAction("CompanyRegistrationPaymentDone", "Company");
                        }
                    }
                }
                return RedirectToAction("Registration", "company");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
            
        }


        
    }
}
