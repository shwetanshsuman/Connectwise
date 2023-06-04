using ConnectwiseWebApplication.CommonMethods;
using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace ConnectwiseWebApplication.Gateways
{
    public class UserFunctions : IUserFunctions
    {
        BranchFunction branchFunction = new BranchFunction();
        CookieOptions cookieOptions = new CookieOptions();
        public async Task<int> ValidateUser(User user, HttpContext httpContext)
        {
            if (user == null)
            {
                return 0;
            }
            else
            {
                IEnumerable<User> users = await GetUsers();

                string Password = PasswordEncoding.EncodePasswordToBase64(user.Password);

                User currentUser = users.FirstOrDefault(c => c.UserName == user.UserName && c.Password == Password);

                if (currentUser != null)
                {
                    httpContext.Response.Cookies.Append("user_Id", currentUser.UserId.ToString(), cookieOptions);
                    httpContext.Response.Cookies.Append("UserName", currentUser.UserName.ToString(), cookieOptions);

                    cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(30));
                    string rolename = currentUser.RoleId == 2 ? "Admin" : "User";
                    List<Claim> claims = new List<Claim>()
                    {
                            new Claim(ClaimTypes.NameIdentifier, user.UserName),
                            new Claim(ClaimTypes.Role, rolename)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        /*IsPersistent = user.,*/
                        ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                    };
                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
					if (currentUser.RoleId == 2)
					{
						return 2;
					}
					else if (currentUser.RoleId == 3)
					{
						return 3;
					}
					else
					{
						return -1;
					}
				}
                else
                {
                    return -1;
                }

            }

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                    return (users);
                }
            }
        }

        public async Task<User> GetUserById(int? id)
        {
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Users/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                    return (user);
                }
            }
        }

        public async Task<int> GetCompanyIdByUserId(int userId)
        {
            var user = await GetUserById(userId);
            int branchId = user.BranchId;
            var branch = await branchFunction.GetBranchesById(branchId);
            int companyId = branch.CompanyId;
            return companyId;

        }

        public async Task<int> CreateUser(User user, HttpContext httpContext)
        {
            UserFunctions userFunctions =new UserFunctions(); 
           IEnumerable<User>  users;
            users = await userFunctions.GetUsers();
            User curreuser=users.FirstOrDefault(x=>x.UserName==user.UserName);
            if (curreuser!= null) 
            {
                return -4;
                    
            }
            
            using (var httpClient1 = new HttpClient())
            {
               
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = DateTime.Now;
                int? id = Convert.ToInt32(httpContext.Request.Cookies["user_Id"].ToString());
                User useradmin = await GetUserById(id);
                user.UpdatedBy = useradmin.UserName;
                user.CreatedBy = useradmin.UserName;
                user.RoleId = 3;
                user.Password= PasswordEncoding.EncodePasswordToBase64(user.Password);
               
               

                httpClient1.BaseAddress = new Uri("https://localhost:7266/api/");
                var posttask = httpClient1.PostAsJsonAsync("Users", user);
                posttask.Wait();
                
                if (posttask.Result.IsSuccessStatusCode)
                {
                    return 1;
                }
                else
                    return -1;
            }
        }

		public async Task<bool> DeleteUser(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:7266/api/Users/" + userId);
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

		public async Task<IEnumerable<Transaction>> GetUserTransactions(HttpContext httpContext)
		{
			int? id = Convert.ToInt32(httpContext.Request.Cookies["user_Id"].ToString());

			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://localhost:7266/api/Transactions/"))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					IEnumerable<Transaction> transaction = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(apiResponse);
					transaction = transaction.Where(x => x.PayerId == id);
					return transaction;
				}
			}
		}

        public async Task<bool> UpdateUser(int userId, User user)
        {
            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = user.UserName;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync("https://localhost:7266/api/Users/" + userId,user);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateUserPassword(int userId, string password)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync("https://localhost:7266/api/PasswordReset/" + userId, password);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }


       

    }
}
