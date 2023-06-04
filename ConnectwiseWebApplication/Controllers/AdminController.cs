using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ConnectwiseWebApplication.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        UserFunctions userFunctions = new UserFunctions();
        InvoiceFunction invoiceFunctions = new InvoiceFunction();
        BranchFunction branchFunctions = new BranchFunction();
        CookieOptions cookieOptions = new CookieOptions();
        public async Task<IActionResult> Home()
        {
            try
            {
                int? id = Convert.ToInt32(HttpContext.Request.Cookies["user_Id"].ToString());
                IEnumerable<User> users;
                IEnumerable<Invoice> invoices;
                IEnumerable<Branch> branches;

                string companyEmail;

                User user = await userFunctions.GetUserById(id);
                ViewData["UserName"] = user.UserName;
                ViewData["Branch"] = user.BranchId;
                Branch branch = await branchFunctions.GetBranchesById(user.BranchId);
                HttpContext.Response.Cookies.Append("Company_id", branch.CompanyId.ToString(), cookieOptions);
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(30));

                companyEmail = user.UpdatedBy;

                users = await userFunctions.GetUsers();
                users = users.Where(c => c.CreatedBy == user.CreatedBy && c.UserName != companyEmail).ToList();

                invoices = await invoiceFunctions.GetInvoicesByCompanyID(HttpContext);
                branches = await branchFunctions.GetBranchesByComapnyId(user.BranchId);
                branches = branches.Where(x => x.IsActive == true);
                IEnumerable<Invoice> PendingInvoices = invoices.Where(x => x.Status == 0);
                invoices = invoices.Where(x => x.Status == 1);
                dynamic mymodel = new ExpandoObject();
                mymodel.Users = users;
                mymodel.Invoices = invoices;
                mymodel.Pendinginvoices = PendingInvoices;
                mymodel.Branches = branches;
                ViewData["TotalUsers"] = users.Count();
                ViewData["TotalInvoices"] = invoices.Count() + PendingInvoices.Count();
                ViewData["PendingInvoices"] = PendingInvoices.Count();
                ViewData["TotalBranches"] = branches.Count();
                return View(mymodel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Error");
            }
        }
    }
}
