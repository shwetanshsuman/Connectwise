using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Controllers;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConnectwiseWebApplication.Gateways
{
    public class CompanyFuntions : ICompanyFunctions
    {
        CookieOptions cookieOptions = new CookieOptions();
        public int CreateCompany(string companyName, string companyEmail, string adminPassword)
        {
            Company company = new Company()
            {
                Name = companyName,
                Email = companyEmail,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                SubscriptionId = 1,
                IsDeleted = false


            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var postTask = client.PostAsJsonAsync<Company>("Companies", company);
                postTask.Wait();
                var result = postTask.Result;
                var res = result.Content.ReadAsStringAsync().Result;
                Company comp = JsonConvert.DeserializeObject<Company>(res);
                if (result.IsSuccessStatusCode)
                {
                    //CreateAccountAdmin(comp.CompanyId, email, password);
                    return comp.CompanyId;

                }
                else
                {
                    return -1;
                }


            }
        }
        //Add branch and remove relation of company with user
        public int CreateAccountAdmin(int branchId, string companyEmail, string adminPassword, HttpContext httpContext)
        {
            string Password = PasswordEncoding.EncodePasswordToBase64(adminPassword);
            User user = new User()
            {
                UserName = companyEmail,
                Password = Password,
                BranchId = branchId,
                CreatedDate = DateTime.Now,
                UpdatedBy = companyEmail,
                CreatedBy = companyEmail,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                RoleId = 2,

            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var postTask = client.PostAsJsonAsync<User>("Users", user);
                postTask.Wait();
                var result = postTask.Result;
                var res = result.Content.ReadAsStringAsync().Result;
                User u = JsonConvert.DeserializeObject<User>(res);
                int userid = u.UserId;
                if (result.IsSuccessStatusCode)
                {

                    return userid;

                }
                return -1;
            }
        }

        public int CreateBranch(int companyId, string BranchName, string street, string landmark, string city, string state, string country, int pincode)
        {
            Branch branch = new Branch()
            {
                BranchName = BranchName,
                City = city,
                State = state,
                Country = country,
                Pincode = pincode,
                CompanyId = companyId,
                LandMark = landmark,
                IsActive = true,
                Street = street,

            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var postTask = client.PostAsJsonAsync<Branch>("Branches", branch);
                postTask.Wait();
                var result = postTask.Result;
                var res = result.Content.ReadAsStringAsync().Result;
                Branch branchdata = JsonConvert.DeserializeObject<Branch>(res);
                if (result.IsSuccessStatusCode)
                {
                    //CreateAccountAdmin(comp.CompanyId, email, password);
                    return branchdata.BranchId;

                }
                else
                {
                    return -1;
                }


            }
        }

        public async Task<Tuple<int, int>> Compnayregistrationwithsubscription(Registration registration, HttpContext httpContext)
        {
            //var registration = JsonConvert.DeserializeObject<Registration>(data);
            // var registrationPayment = JsonConvert.DeserializeObject<RegistrationPayment>(registrationPaymentstr);




            var companyId = CreateCompany(registration.companyName, registration.companyEmail, registration.adminPassword);
            if (companyId != -1)
            {
                
                //httpContext.Response.Cookies.Append("Company_id", companyId.ToString(), cookieOptions);
                //cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(30));
                //int? id = Convert.ToInt32(httpContext.Request.Cookies["Company_id"].ToString());
                var BranchId = CreateBranch(companyId, registration.BranchName, registration.street, registration.LandMark, registration.city, registration.state, registration.country, registration.pincode);
                if (BranchId != -1)
                {
                    int userid = CreateAccountAdmin(BranchId, registration.companyEmail, registration.adminPassword, httpContext);
                    if (userid != -1)
                    { 

                        MasterEmail masterEmail;
                        EmailFunction emailFunction = new EmailFunction();
                        masterEmail =  await emailFunction.GetEmailById(1);
                        string body = masterEmail.EmailBody;
                        string redirectToLogin = "https://localhost:7081/";
                        body = String.Format(body, registration.companyName, redirectToLogin);

                        Email email = new Email()
                        {
                            MailFromName = "Connectwise Admin",
                            MailFrom = "connectwisetestfunction@gmail.com",
                            MailTo = registration.companyEmail,
                            Subject = masterEmail.EmailSubject,
                            Body = body
                        };
                        EmailExecution emailExecution = new EmailExecution();
                        emailExecution.SendEmail(email);
                        return Tuple.Create(userid,companyId);
                    }
                }


            }
            return null;

        }

        public async Task<bool> CheckEmail(string email)
        {
            IEnumerable<User> users = new List<User>();
            UserFunctions userFunctions = new UserFunctions();
            users = await userFunctions.GetUsers();
            User user = users.FirstOrDefault(x => x.UserName == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Company> GetCompanyByid(int id)
        {
            Company company = new Company();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Companies/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    company = JsonConvert.DeserializeObject<Company>(apiResponse);
                    return (company);
                }

            }
        }

        public bool updateCompany(Company company,int subcriptionid)
        {
            company.SubscriptionId= subcriptionid;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var puttask = client.PutAsJsonAsync("Companies/"+company.CompanyId, company);
                puttask.Wait();
                if (puttask.Result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
                return false;
        }
    }
}
