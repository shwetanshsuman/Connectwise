using Newtonsoft.Json;
using ConnectwiseWebApplication.Models;
using ConnectWiseBackend.Models;
using Branch = ConnectwiseWebApplication.Models.Branch;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ConnectwiseWebApplication.Gateways
{
    public class BranchFunction : IBranchFunction
    {
        public async Task<IEnumerable<Branch>> GetBranches()
        {
            IEnumerable<Branch> branches;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7266/api/Branches"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    branches = JsonConvert.DeserializeObject<List<Branch>>(apiResponse);
                }
            }
            return branches;
        }



        public  async Task<IEnumerable<Branch>> GetBranchesByComapnyId(int branchId)
        {
            Branch branch = await GetBranchesById(branchId);
            int compnayId = branch.CompanyId;

            IEnumerable<Branch> branches;
            branches = await GetBranches();
            branches=branches.Where(x =>x.CompanyId   == compnayId);
            return branches;
            
        }

        public async Task<Branch> GetBranchesById(int branchId)
        {
           Branch branch=null;
           using(var httpClient = new HttpClient())

            using (var response = await httpClient.GetAsync("https://localhost:7266/api/Branches/"+branchId))
            {
                try
                { 
                string apiResponse = await response.Content.ReadAsStringAsync();
                branch = JsonConvert.DeserializeObject<Branch>(apiResponse);
                }
                catch (Exception )
                {

                }
             }
          
            return branch;

        }

        public async Task<bool> DeleteBranch(int branchId)
        {
            using(var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:7266/api/Branches/" + branchId);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public bool EditBranch(Branch branch)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7266/api/");
                var puttask = client.PutAsJsonAsync("Branches/" + branch.BranchId, branch);
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

