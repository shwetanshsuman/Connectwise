using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace ConnectwiseWebApplication.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        InvoiceFunction InvoiceFunction = new InvoiceFunction();
        TransactionFunction TransactionFunction = new TransactionFunction();
        UserFunctions UserFunctions = new UserFunctions();

        public ActionResult InvoiceForm()
        {
            ViewData["emailerror"] = TempData["Adminemail"] != null ? (int)TempData["Adminemail"] : 0;

            ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();

            return View();
        }


        public async Task<IActionResult> Payment(int id)
        {
            try
            {
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                int errorcode = 0;
                InvoiceFunction invoiceFunction = new InvoiceFunction();
                Invoice invoice = await invoiceFunction.GetInvoiceById(id);
                errorcode = Convert.ToInt32(TempData["Invoiceerror"] as string);
                TempData["InvoiceDetails"] = JsonConvert.SerializeObject(invoice);
                ViewData["InvoiceTitle"] = invoice.InvoiceTitle;
                ViewData["InvoiceTotamount"] = invoice.TotalAmount;
                ViewData["InvoiceDueAmount"] = invoice.DueAmount;
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
                        return RedirectToAction("Index", "User");
                    }

                }
                TempData["Invoiceerror"] = "1";
                return RedirectToAction("Payment", "Invoices", new { id = invoice.InvoiceId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GeneratedInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                ReminderForm reminderForm;
                InvoiceFunction invoiceFunction = new InvoiceFunction();
                IEnumerable<User> users;
                users = await UserFunctions.GetUsers();
                IEnumerable<User> admincheck = users.Where(x => x.UserName == invoiceForm.GeneratedForEmail && x.RoleId == 2).ToList();
                if (admincheck.Count() != 0)
                {
                    TempData["Adminemail"] = -1;
                    return RedirectToAction("InvoiceForm");
                }

                reminderForm = await invoiceFunction.CreateInvoice(invoiceForm, HttpContext);
                if (reminderForm != null)
                {
                    int i = await invoiceFunction.CreateReminder(reminderForm, HttpContext);
                    if (i != -1)
                    {
                        return RedirectToAction("Home", "admin");
                    }
                }
                return RedirectToAction("Index", "Error");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }



        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                Invoice invoice = await InvoiceFunction.GetInvoiceById(id);
                ViewData["title"] = invoice.InvoiceTitle;
                ViewData["status"] = invoice.Status;
                ViewData["totalamount"] = invoice.TotalAmount;
                ViewData["DueAmount"] = invoice.DueAmount;
                ViewData["Genratedforemail"] = invoice.GeneratedForEmail;
                ViewData["createdDate"] = invoice.CreatedDate;
                ViewData["DueDate"] = invoice.DueDate;

                IEnumerable<Transaction> Transactions = await TransactionFunction.GetTransactionsByInvoiceId(id);

                return View(Transactions);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

    }
}
