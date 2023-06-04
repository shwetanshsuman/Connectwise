using ConnectwiseWebApplication.Gateways;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Controllers
{
    public class BranchController : Controller
    {
        CompanyFuntions CompanyFuntions = new CompanyFuntions();
        UserFunctions userFunctions = new UserFunctions();
        BranchFunction branchFunction = new BranchFunction();

        // GET: BranchController
        public ActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
            return View();
        }

        public async Task<IActionResult> AddBranch(Registration registration)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Request.Cookies["user_Id"].ToString());
                int companyId = await userFunctions.GetCompanyIdByUserId(userId);
                var BranchId = CompanyFuntions.CreateBranch(companyId, registration.BranchName, registration.street, registration.LandMark, registration.city, registration.state, registration.country, registration.pincode);
                if (BranchId != -1)
                    return RedirectToAction("Home", "Admin");
                else
                    return View("Index");
            }
            catch(Exception) {
                return RedirectToAction("Index", "Error");
            }
        }


        public async Task<IActionResult> BranchDetails(int id)
        {
            try
            {
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                Branch branch = await branchFunction.GetBranchesById(id);
                ViewData["Branchname"] = branch.BranchName;
                ViewData["address"] = "" + branch.LandMark + " " + branch.Street + " " + branch.City + " " + branch.State + " " + branch.Country + " " + branch.Pincode;
                ViewData["branchid"] = id;
                ViewData["Editmsg"] = TempData["Editbrancherror"];
                IEnumerable<User> user = await userFunctions.GetUsers();
                user = user.Where(x => x.BranchId == branch.BranchId);
                return View(user);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // GET: BranchController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ViewData["UserName"] = HttpContext.Request.Cookies["UserName"].ToString();
                Branch branch = await branchFunction.GetBranchesById(id);
                TempData["Branch"] = JsonConvert.SerializeObject(branch);
                return View(branch);
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // POST: BranchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Branch branch)
        {
            try
            {
                var datastr = TempData["Branch"] as string;
                Branch BranchData = JsonConvert.DeserializeObject<Branch>(datastr);
                branch.BranchId = BranchData.BranchId;
                branch.BranchName = BranchData.BranchName;
                branch.CompanyId = BranchData.CompanyId;
                branch.IsActive = true;
                var res = branchFunction.EditBranch(branch);

                if (res)
                {
                    TempData["Editbrancherror"] = 1;

                }
                else
                {
                    TempData["Editbrancherror"] = -1;
                }
                return RedirectToAction("BranchDetails", "Branch", new { id = branch.BranchId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }

        }

    }
}
