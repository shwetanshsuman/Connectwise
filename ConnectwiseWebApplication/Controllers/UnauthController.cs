
using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Controllers
{
    public class UnauthController : Controller
    {
        // GET: UnAuthUsers
        public async Task<IActionResult> Payment(int id)
        {
            try
            {
                int errorcode = 0;
                InvoiceFunction invoiceFunction = new InvoiceFunction();
                Invoice invoice = await invoiceFunction.GetInvoiceById(id);
                //errorcode = Convert.ToInt32(TempData["Invoiceerror"] as string);
                TempData["InvoiceDetails"] = JsonConvert.SerializeObject(invoice);
                ViewData["InvoiceTitle"] = invoice.InvoiceTitle;
                ViewData["InvoiceTotamount"] = invoice.TotalAmount;
                ViewData["InvoiceDueAmount"] = invoice.DueAmount;
                TempData["Emailfor"] = invoice.GeneratedForEmail;
                ViewData["errorcode"] = errorcode;

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> PaymentGateway(PaymentModel paymentModel)
        {
            try
            {
                var invoicestr = TempData["InvoiceDetails"] as string;
                Invoice invoice = JsonConvert.DeserializeObject<Invoice>(invoicestr);
                InvoiceFunction invoiceFunction = new InvoiceFunction();

                if (invoice != null)
                {
                    int res = await invoiceFunction.InvoiceUserPayment(paymentModel, invoice, HttpContext);
                    if (res != -1)
                    {
                        TempData["paymentsucesscode"] = "1";
                        return RedirectToAction("Paysuccess", "Unauth");
                    }
                }
                TempData["Invoiceerror"] = "1";
                return RedirectToAction("Payment", "Unauth", new { id = invoice.InvoiceId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult Paysuccess()
        {
            return View();
        }
    }
}
