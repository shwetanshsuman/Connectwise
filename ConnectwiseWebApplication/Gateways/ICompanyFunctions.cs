using ConnectwiseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectwiseWebApplication.Gateways
{
    public interface ICompanyFunctions
    {

        int CreateCompany(string companyName,string companyEmail,string adminPassword);
        int CreateBranch(int companyId, string BranchName,string street, string landmark, string city, string state, string country, int pincode);
        int CreateAccountAdmin(int companyId,string companyEmail, string adminPassword,HttpContext httpContext);
        Task<Tuple<int,int>> Compnayregistrationwithsubscription(Registration registration, HttpContext httpContext);
        Task<bool> CheckEmail(string email);

        Task<Company> GetCompanyByid(int id);


        bool updateCompany(Company company,int subcriptionid); 
    }
}
