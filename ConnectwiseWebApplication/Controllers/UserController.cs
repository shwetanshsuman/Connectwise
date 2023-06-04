using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using User = ConnectwiseWebApplication.Models.User;

namespace ConnectwiseWebApplication.Controllers
{
    public class UserController : Controller
    {
        UserFunctions userFunctions = new UserFunctions();
        BranchFunction branchFunctions = new BranchFunction();
        UserPanelModels invoiceFilteration = new UserPanelModels();
        // GET: UserController
        [Authorize(Roles ="User")]
        public async  Task<IActionResult> Index()
        {
            try
            {
                int? id = Convert.ToInt32(HttpContext.Request.Cookies["user_Id"].ToString());

                User user = await userFunctions.GetUserById(id);
                ViewData["UserName"] = user.UserName;
                ViewData["Branch"] = user.BranchId;
                ViewData["Paymentstatus"] = TempData["paymentsucesscode"] as string;
                InvoiceFunction invoiceFunction = new InvoiceFunction();
                IEnumerable<Invoice> invoices = await invoiceFunction.GetInvoicesByuserID((int)id);
                IEnumerable<Transaction> transactions = await userFunctions.GetUserTransactions(HttpContext);
                UserPanelModels userPanelModels = new UserPanelModels();
                userPanelModels.CompletedInvoice = invoices.Where(x => x.Status == 1);
                userPanelModels.PendingInvoice = invoices.Where(x => x.Status == 0);
                userPanelModels.Transactions = transactions;
                ViewData["TotalInvoices"] = invoices.Count();
                ViewData["PendingInvoices"] = userPanelModels.PendingInvoice.Count();
                ViewData["Transactions"] = transactions.Count();
                return View(userPanelModels);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
 

        // GET: UserController/Create

        public async Task<ActionResult> Create()
        {

            try
            {
                ViewData["error"] = TempData["emailerror"] != null ? (int)TempData["emailerror"] : 0;
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                BranchFunction branchFunction = new BranchFunction();
                int? CompanyId = Convert.ToInt32(HttpContext.Request.Cookies["Company_id"].ToString());
                IEnumerable<Branch> branches = await branchFunction.GetBranches();
                branches = branches.Where(branch => branch.CompanyId == CompanyId);
                ViewBag.Branches = branches;


                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // POST: UserController/AddUser
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                var result = await userFunctions.CreateUser(user,HttpContext);

                if(result != -1 && result!=-4)
                { 
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    if(result==-4)
                    {
                        TempData["emailerror"] = -1;
                    }

                    return RedirectToAction("Create");
                }
            }

            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // GET: UserController/Edit/5
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Edit()
        {
            try
            {
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                int? id = Convert.ToInt32(HttpContext.Request.Cookies["user_Id"].ToString());
                UserFunctions userFunctions = new UserFunctions();
                User user = await userFunctions.GetUserById(id);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> EditDetails(User user)
        {
            try
            {
                user.UpdatedDate = DateTime.Now;
                user.UpdatedBy = user.UserName;
                UserFunctions userFunctions = new UserFunctions();
                bool response = await userFunctions.UpdateUser(user.UserId, user);
                if (response)
                {
                    return RedirectToAction("Index", "User");
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
